using PaymentSense.Models.ServicesClient;

namespace PaymentSense.DataLayer.HttpClient
{
    public interface IServiceClient
    {
        Task<IEnumerable<RestCountry>> GetAllCountriesAsync();
    }
}
