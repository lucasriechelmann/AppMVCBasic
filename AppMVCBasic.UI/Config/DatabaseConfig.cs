using AppMVCBasic.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppMVCBasic.UI.Config
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<AspMVCBasicDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
