using Application.Commons;
using DiamondShopSystem_SWD392_;
using DiamondShopSystem_SWD392_.Middlewares;
using Google.Apis.Auth.OAuth2;
using Infrastructures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Net.payOS;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
builder.Services.AddInfrastructuresService(configuration.DatabaseConnection);
builder.Services.AddWebAPIService();


// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration.JWTSection.Issuer,
            ValidAudience = configuration.JWTSection.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWTSection.SecretKey)),
        };
    });

//PayOS
PayOS payOs = new PayOS(configuration.PayOSConfig.PAYOS_CLIENT_ID,
                        configuration.PayOSConfig.PAYOS_API_KEY,
                        configuration.PayOSConfig.PAYOS_CHECKSUM_KEY);
builder.Services.AddSingleton(payOs);
//End PayOS

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

/*
    register with singleton life time
    now we can use dependency injection for AppConfiguratio
*/
builder.Services.AddSingleton(configuration);
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

//Firebase
var google = JObject.FromObject(configuration.GoogleImage);
string g = google.ToString();
string temp = Path.GetTempFileName();
File.WriteAllText(temp, g);
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", temp);
GoogleCredential credential = GoogleCredential.FromFile(temp);
//End FireBase
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Chỉ cho phép nguồn gốc này
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Cho phép cookie và token
    });
});



var app = builder.Build();
app.UseCors("AllowReactApp");

app.UseRouting();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<GlobalExceptionMiddleware>();
//app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<ConfirmationTokenMiddleware>();
app.MapHealthChecks("/healthchecks");
app.UseHttpsRedirection();

// todo authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// this line tell intergrasion test
// https://stackoverflow.com/questions/69991983/deps-file-missing-for-dotnet-6-integration-tests
public partial class Program { }