using System.Threading.Tasks;

namespace CovidRapidApi.Domain.Services
{
    public interface IContinentsService
    {
        Task<string> GetContinentData();
    }
}