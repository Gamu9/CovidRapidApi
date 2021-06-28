using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CovidRapidApi.Domain.Models;
using Newtonsoft.Json;


namespace CovidRapidApi.Domain.Services
{
    public class StatisticsService : IStatisticsService
    {
        public async Task<StatisticsValues> FetchStatisticsData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-193.p.rapidapi.com/statistics"),
                Headers =
                {
                    { "x-rapidapi-key", "c11c447488msh73041562e38181dp1fb052jsnb9bd1f1950f4" },
                    { "x-rapidapi-host", "covid-193.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<StatisticsValues>(body);
                return results;
            }

        }
    }
}
