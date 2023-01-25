using PaymentSense.Models.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentSense.BusinessLayer
{
    public interface ICountryManager
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByCalling(string callingCode);

    }
}
