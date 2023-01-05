using Microsoft.AspNetCore.Mvc;

namespace AppMVCBasic.UI.Config
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services
                .AddControllersWithViews(o =>
                {
                    o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .AddRazorRuntimeCompilation();
            return services;
        }
    }
}
