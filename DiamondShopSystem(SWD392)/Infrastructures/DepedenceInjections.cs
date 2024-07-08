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

            
            services.AddScoped<ICurrentTime, CurrentTime>();
            //account
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            //promotion
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            //authen
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //diamond
            services.AddScoped<IDiamondService, DiamondService>();
            services.AddScoped<IDiamondRepository, DiamondRepository>();
            //payment
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            //cart
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
            //order
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //product
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //category
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //productType
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            //ProductDiamond
            services.AddScoped<IProductDiamondRepository, ProductDiamondRepository>();

            //Firebase
            services.AddSingleton(opt => StorageClient.Create());
            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
            //Image
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageRepository, ImageRepository>();
            

            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(databaseConnection);
            });

            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
