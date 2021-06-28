using System.Threading.Tasks;

namespace CovidRapidApi.Domain.Services
{
    public interface ICountryService
    {
        Task<string> GetCountriesData();
    }
}