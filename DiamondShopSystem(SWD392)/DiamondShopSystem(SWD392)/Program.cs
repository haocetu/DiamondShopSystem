using Application.Commons;
using DiamondShopSystem_SWD392_;
using DiamondShopSystem_SWD392_.Middlewares;
using Infrastructures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
builder.Services.AddInfrastructuresService(configuration.DatabaseConnection);
builder.Services.AddWebAPIService();

// Load JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("JWTSection").Get<JWTSection>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
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
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Customer", policy =>
        policy.RequireClaim("RoleID", "1"));
    options.AddPolicy("Admin", policy =>
        policy.RequireClaim("RoleID", "2"));
    options.AddPolicy("Staff", policy =>
        policy.RequireClaim("RoleID", "3"));
});
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
//Firebase
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"D:\diamondshopswd392-firebase-adminsdk-zyw79-c676ac56f2.json");

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