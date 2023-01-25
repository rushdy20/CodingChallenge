using AutoMapper;
using PaymentSense.Models.Internal;
using PaymentSense.Models.ServicesClient;

namespace PaymentSense.Mappers
{
    public class GetCountryInternalProfilerToGetRestCountryProfiler : Profile
    {
        public GetCountryInternalProfilerToGetRestCountryProfiler()
        {
            CreateMap<RestCountry, Country>(MemberList.None)
                .ForMember(d => d.CallingCodes, m => m.MapFrom(s => s.CallingCode))
                .ForMember(d => d.Capital, m => m.MapFrom(s => s.Capital))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Region, m => m.MapFrom(s => s.Region));
        }

    }
}
