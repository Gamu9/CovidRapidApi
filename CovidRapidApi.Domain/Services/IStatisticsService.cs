using System.Threading.Tasks;
using CovidRapidApi.Domain.Models;

namespace CovidRapidApi.Domain.Services
{
    public interface IStatisticsService
    {
        Task<StatisticsValues> FetchStatisticsData();
    }
}