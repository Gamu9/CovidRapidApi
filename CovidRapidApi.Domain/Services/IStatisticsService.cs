using CovidRapidApi.Domain.Models;
using System.Threading.Tasks;

namespace CovidRapidApi.Domain.Services
{
    public interface IStatisticsService
    {
        Task<StatisticsValues> FetchStatisticsData();
    }
}