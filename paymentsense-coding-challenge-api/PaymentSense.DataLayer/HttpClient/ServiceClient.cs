using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentSense.DataLayer.Constant;
using PaymentSense.DataLayer.Extenstion;
using PaymentSense.Models.ServicesClient;
using System.Net;
using System.Text;

namespace PaymentSense.DataLayer.HttpClient
{
    public class ServiceClient : IServiceClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly ILogger<ServiceClient> _logger;
        private readonly IOptions<AppSettings> _appSettings;

        public ServiceClient(
            ILogger<ServiceClient> logger,
            System.Net.Http.HttpClient httpClient,
            IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

        }
        public async Task<IEnumerable<RestCountry>> GetAllCountriesAsync()
        {
            _logger.LogInformation(LogEventNames.DataLayer.ServiceClient.GetAllCountries.Enter);

            var responseMessage = await SendRequest(_appSettings.Value.ServiceUrl, string.Empty, HttpMethod.Get);

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogInformation(LogEventNames.DataLayer.ServiceClient.GetAllCountries.ResponseNotValid);

                responseMessage.StatusCode.CheckForCommonError();

            }
            var response = JsonConvert.DeserializeObject<List<RestCountry>>(await responseMessage.Content.ReadAsStringAsync());

            _logger.LogInformation(LogEventNames.DataLayer.ServiceClient.GetAllCountries.Exit);

            return response;
        }

        private async Task<HttpResponseMessage> SendRequest(string serviceEndPoint, string jsonStringContent, HttpMethod httpMethod)
        {
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri(serviceEndPoint),
                    Content = new StringContent(jsonStringContent, Encoding.UTF8, "application/json"),
                    Method = httpMethod
                };

                return await _httpClient.SendAsync(requestMessage);
            }
            catch (Exception e)
            {
                _logger.LogError($"{LogEventNames.DataLayer.ServiceClient.SendRequest.Exception} - {e.InnerException}");
            }

            return new HttpResponseMessage();
        }
    }
}
