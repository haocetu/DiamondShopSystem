using Application.Commons;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Google.Cloud.Storage.V1;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class DepedenceInjections
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {

            services.AddScoped<IRoleService, RoleService>();
            
            services.AddScoped<ICurrentTime, CurrentTime>();
            
            services.AddScoped<IAccountService, AccountService>();
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            services.AddScoped<IDiamondService, DiamondService>();
            
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<ICartService, CartService>();
            
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            //Firebase
            services.AddSingleton(opt => StorageClient.Create());
            
            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
            
            services.AddScoped<IImageService, ImageService>();
            
            services.AddScoped<ICartService, CartService>();

            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(databaseConnection);
            });

            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
