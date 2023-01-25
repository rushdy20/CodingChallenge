using Microsoft.Extensions.DependencyInjection;
using PaymentSense.DataLayer.HttpClient;
using Polly;
using System.Net.Http.Headers;

namespace PaymentSense.DataLayer
{
    public static class DataLayerModule
    {
        public static IServiceCollection AddDataLayerModule(this IServiceCollection services)
        {
            services
                .AddHttpClient<IServiceClient, ServiceClient>(c =>
                {
                    c.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30)));



            return services;
        }
    }
}
