using Microsoft.Extensions.DependencyInjection;

namespace PaymentSense.BusinessLayer
{
    public static class BusinessLayerModule
    {
        public static IServiceCollection AddBusinessLayerModule(this IServiceCollection services)
        {
            services
                .AddSingleton<ICountryManager, CountryManager>()
                .AddSingleton<ICacheManager, CacheManager>();
            return services;
        }
    }
}
