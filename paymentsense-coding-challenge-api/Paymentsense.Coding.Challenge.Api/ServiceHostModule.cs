using Microsoft.Extensions.DependencyInjection;
using PaymentSense.BusinessLayer;
using PaymentSense.DataLayer;

namespace Paymentsense.Coding.Challenge.Api
{
    public static class ServiceHostModule
    {
        public static IServiceCollection AddServiceHostModule(this IServiceCollection services)
        {
            services.AddBusinessLayerModule();
            services.AddDataLayerModule();
            return services;
        }
    }
}
