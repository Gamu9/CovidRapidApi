using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CovidRapidApi.Domain.Models;
using Newtonsoft.Json;

namespace CovidRapidApi.Domain.Services
{
    public class ContinentsService : IContinentsService
    {
        public async Task<string> GetContinentData()
        {
            var statisticsService = new StatisticsService();
            var statisticsValues = await statisticsService.FetchStatisticsData();

            if (statisticsValues?.Response != null && statisticsValues.Response.Count > 0)
            {
                var continentValues = GetContinentsValues(statisticsValues.Response);
                var results = CalculateContinentData(continentValues);
                return JsonConvert.SerializeObject(results);
            }

            return "";
        }

        private IEnumerable<ContinentValues> GetContinentsValues(List<Response> response)
        {
            try
            {
                var continentValues = new List<ContinentValues>();
                foreach (var item in response)
                {
                    if (!string.IsNullOrWhiteSpace(item.Continent))
                    {
                        continentValues.Add(new ContinentValues
                        {
                            Continent = item.Continent.Trim(),
                            NewCases = item.Cases == null ? 0 : Convert.ToInt32(item.Cases.New),
                            ActiveCases = item.Cases == null ? 0 : Convert.ToInt32(item.Cases.Active),
                            Deaths = Convert.ToInt32(item.Deaths.total)
                        });
                    }

                }
                return continentValues;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private IEnumerable<ContinentsResults> CalculateContinentData(IEnumerable<ContinentValues> continentValues)
        {
            var continentsResults = new List<ContinentsResults>();
            try
            {
                int globalNewCasesTotal = 0;
                int globalActiveCasesTotal = 0;
                int globalDeathsTotal = 0;

                if (continentValues.Any())
                {
                    globalNewCasesTotal = (int)continentValues.Sum(x => x.NewCases);
                    globalActiveCasesTotal = (int)continentValues.Sum(x => x.ActiveCases);
                    globalDeathsTotal = (int)continentValues.Sum(x => x.Deaths);
                }

                continentsResults.Add(new ContinentsResults
                {
                    Continent = "Global",
                    NewCases = new NewCases
                    {
                        Total = globalNewCasesTotal,
                        Percent = 100,
                    },
                    Active = new ActiveCases
                    {
                        Total = globalActiveCasesTotal,
                        Percent = 100,
                    },
                    Deaths = new DeathCases
                    {
                        Total = globalDeathsTotal,
                        Percent = 100,
                    }
                });

                var distinctContinents = continentValues.Select(x => x.Continent).Distinct().OrderBy(x => x); 

                foreach (var continent in distinctContinents)
                {
                    var newCasesTotal = (int)continentValues.Where(x => x.Continent.Equals(continent)).Select(x => x.NewCases).ToList().Sum();
                    var newCasesPercent = GetPercentage(newCasesTotal, globalNewCasesTotal);
                    var activeCasesTotal = (int)continentValues.Where(x => x.Continent.Equals(continent)).Select(x => x.ActiveCases).ToList().Sum();
                    var activeCasesPercent = GetPercentage(activeCasesTotal, globalActiveCasesTotal);
                    var deathCasesTotal = (int)continentValues.Where(x => x.Continent.Equals(continent)).Select(x => x.Deaths).ToList().Sum();
                    var deathCasesPercent = GetPercentage(deathCasesTotal, globalDeathsTotal);
                    continentsResults.Add(new ContinentsResults
                    {
                        Continent = continent,
                        NewCases = new NewCases
                        {
                            Total = newCasesTotal,
                            Percent = newCasesPercent,
                        },
                        Active = new ActiveCases
                        {
                            Total = activeCasesTotal,
                            Percent = activeCasesPercent,
                        },
                        Deaths = new DeathCases
                        {
                            Total = deathCasesTotal,
                            Percent = deathCasesPercent,
                        }
                    });
                }

                return continentsResults;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private int GetPercentage(long value, long total)
        {
            if (value == 0 || total == 0) return 0;
            var percentage =  ((double)value / total) * 100;
            return Convert.ToInt32(percentage);
        }
    }
}
