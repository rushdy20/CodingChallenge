using AutoMapper;
using Microsoft.Extensions.Logging;
using PaymentSense.BusinessLayer.Constants;
using PaymentSense.DataLayer.HttpClient;
using PaymentSense.Models.Internal;
using PaymentSense.Models.ServicesClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSense.BusinessLayer
{

    public class CountryManager : ICountryManager
    {
        private readonly ILogger<CountryManager> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceClient _serviceClient;
        private readonly ICacheManager _cacheManager;

        private const string AllCountryCacheKey = "AllCountryCached";

        public CountryManager(
            ILogger<CountryManager> logger,
            IMapper mapper,
            IServiceClient serviceClient,
            ICacheManager cacheManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            _cacheManager = cacheManager ?? throw new ArgumentNullException(nameof(cacheManager));
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {

            _logger.LogInformation(LogEventNames.CountryManager.GetAllCountry.Enter);

            var getAllCountries = GetFromCahceData(AllCountryCacheKey);

            if (!getAllCountries.Any())
            {
                getAllCountries = await _serviceClient.GetAllCountriesAsync();

                _cacheManager.Set(AllCountryCacheKey, getAllCountries);
            }

            var allCounties = _mapper.Map<List<Country>>(getAllCountries);

            _logger.LogInformation(LogEventNames.CountryManager.GetAllCountry.Exit);

            return allCounties;
        }

        public async Task<Country> GetCountryByCalling(string callingCode)
        {
            _logger.LogInformation(LogEventNames.CountryManager.GetCountryByCallingCode.Enter);

            var cachedData = GetFromCahceData(AllCountryCacheKey);

            if (!cachedData.Any())
            {
                await GetAllCountriesAsync();

                cachedData = GetFromCahceData(AllCountryCacheKey);
            }

            var countryByCallingId = cachedData.FirstOrDefault(a => a.CallingCode == callingCode);

            _logger.LogInformation(LogEventNames.CountryManager.GetCountryByCallingCode.Exit);

            return _mapper.Map<Country>(countryByCallingId);

        }

        private IEnumerable<RestCountry> GetFromCahceData(string key)
        {
            var allcontries = _cacheManager.Get<List<RestCountry>>(AllCountryCacheKey);
            return allcontries ?? new List<RestCountry>();
        }

    }
}
