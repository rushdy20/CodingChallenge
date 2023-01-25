using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PaymentSense.Mappers;

namespace Paymentsense.Coding.Challenge.Api
{
    public static class AutoMapperModule
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GetCountryInternalProfilerToGetRestCountryProfiler>();
                cfg.AddProfile<GetCountryResponseProfilerToGetCountryInternalProfiler>();
            });

            services.AddSingleton(mappingConfig);
            services.AddSingleton(mappingConfig.CreateMapper());

            return services;
        }
    }
}
