using AutoMapper;
using PaymentSense.Models.External.Get.Response;
using PaymentSense.Models.Internal;

namespace PaymentSense.Mappers
{
    public class GetCountryResponseProfilerToGetCountryInternalProfiler : Profile
    {
        public GetCountryResponseProfilerToGetCountryInternalProfiler()
        {
            CreateMap<Country, GetCountriesResponse>(MemberList.None)
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Capital, m => m.MapFrom(s => s.Capital))
                .ForMember(d => d.CountryCallingCode, m => m.MapFrom(s => s.CallingCodes));
        }
    }
}
