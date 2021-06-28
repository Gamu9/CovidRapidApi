using Newtonsoft.Json;
using System;

namespace CovidRapidApi.Domain.Models
{
    public class Response
    {
        [JsonProperty(PropertyName = "continent")]
        public string Continent { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "population")]
        public int? Population { get; set; }

        [JsonProperty(PropertyName = "cases")]
        public Cases Cases { get; set; }

        [JsonProperty(PropertyName = "deaths")]
        public Deaths Deaths { get; set; }

        [JsonProperty(PropertyName = "tests")]
        public Tests Tests { get; set; }

        [JsonProperty(PropertyName = "day")]
        public string Day { get; set; }

        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get; set; }
    }
}