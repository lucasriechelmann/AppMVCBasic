using AppMVCBasic.Business.Interfaces;
using AppMVCBasic.Data.Repositories;
using AppMVCBasic.UI.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace AppMVCBasic.UI.Config
{
    public static class DependencyInjectionsConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, CurrencyValidationAttibuteAdapterProvider>();

            return services;
        }
    }
}
