using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CovidRapidApi.Domain.Models
{
    public class CountryResults
    {
        [JsonProperty(PropertyName = "continent")]
        public string Continent { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "new")]
        public NewCases NewCases { get; set; }

        [JsonProperty(PropertyName = "active")]
        public ActiveCases Active { get; set; }

        [JsonProperty(PropertyName = "deaths")]
        public DeathCases Deaths { get; set; }
    }
}
