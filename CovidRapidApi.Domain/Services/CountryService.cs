using CovidRapidApi.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidRapidApi.Domain.Services
{
    public class CountryService : ICountryService
    {
        public async Task<string> GetCountriesData()
        {
            var statisticsService = new StatisticsService();
            var statisticsValues = await statisticsService.FetchStatisticsData();

            if (statisticsValues?.Response != null && statisticsValues.Response.Count > 0)
            {
                var continentValues = GetCountriesValues(statisticsValues.Response);
                var results = CalculateCountriesData(continentValues);
                return JsonConvert.SerializeObject(results);
            }

            return "";
        }

        private IEnumerable<CountryValues> GetCountriesValues(List<Response> response)
        {
            try
            {
                var continentValues = new List<CountryValues>();
                foreach (var item in response)
                {
                    if (!string.IsNullOrWhiteSpace(item.Continent))
                    {
                        continentValues.Add(new CountryValues
                        {
                            Continent = item.Continent.Trim(),
                            Country = item.Country,
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

        private IEnumerable<CountryResults> CalculateCountriesData(IEnumerable<CountryValues> countryValues)
        {
            var countryResults = new List<CountryResults>();
            try
            {
                var distinctContinents = countryValues.Select(x => x.Continent).Distinct().OrderBy(x => x);

                foreach (var continent in distinctContinents)
                {
                    int continentNewCasesTotal = 0;
                    int continentActiveCasesTotal = 0;
                    int continentDeathsTotal = 0;

                    var continentValues = countryValues.Where(x => x.Continent.Equals(continent));

                    if (continentValues.Any())
                    {
                        continentNewCasesTotal = (int)continentValues.Sum(x => x.NewCases);
                        continentActiveCasesTotal = (int)continentValues.Sum(x => x.ActiveCases);
                        continentDeathsTotal = (int)continentValues.Sum(x => x.Deaths);
                    }

                    countryResults.Add(new CountryResults
                    {
                        Continent = continent,
                        Country = "",
                        NewCases = new NewCases
                        {
                            Total = continentNewCasesTotal,
                            Percent = 100,
                        },
                        Active = new ActiveCases
                        {
                            Total = continentActiveCasesTotal,
                            Percent = 100,
                        },
                        Deaths = new DeathCases
                        {
                            Total = continentDeathsTotal,
                            Percent = 100,
                        }
                    });

                    foreach (var item in continentValues)
                    {
                        var newCasesTotal = (int)continentValues.Where(x => x.Country.Equals(item.Country)).Select(x => x.NewCases).Sum();
                        var newCasesPercent = GetPercentage(newCasesTotal, continentNewCasesTotal);
                        var activeCasesTotal = (int)continentValues.Where(x => x.Country.Equals(item.Country)).Select(x => x.ActiveCases).Sum();
                        var activeCasesPercent = GetPercentage(activeCasesTotal, continentActiveCasesTotal);
                        var deathCasesTotal = (int)continentValues.Where(x => x.Country.Equals(item.Country)).Select(x => x.Deaths).Sum();
                        var deathCasesPercent = GetPercentage(deathCasesTotal, continentDeathsTotal);

                        countryResults.Add(new CountryResults
                        {
                            Continent = continent,
                            Country = item.Country,
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
                }

                return countryResults;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private int GetPercentage(long value, long total)
        {
            if (value == 0 || total == 0) return 0;
            var percentage = ((double)value / total) * 100;
            return Convert.ToInt32(percentage);
        }
    }
}
