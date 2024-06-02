using Infrastructures;
using Microsoft.EntityFrameworkCore;

namespace DiamondShopSystem_SWD392_
{
    public static class DepedenceInjection
    {
        public static IServiceCollection AddDbContextsWithConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DBConnect")!;
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));
            return services;
        }
    }
}
