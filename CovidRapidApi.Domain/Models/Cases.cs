using Newtonsoft.Json;

namespace CovidRapidApi.Domain.Models
{
    public class Cases
    {
        [JsonProperty(PropertyName = "new")]
        public string New { get; set; }
        [JsonProperty(PropertyName = "active")]
        public int? Active { get; set; }
        [JsonProperty(PropertyName = "critical")]
        public int? Critical { get; set; }
        [JsonProperty(PropertyName = "recovered")]
        public int? Recovered { get; set; }
        [JsonProperty(PropertyName = "_1M_pop")]
        public string _1M_pop { get; set; }
        [JsonProperty(PropertyName = "total")]
        public int? Total { get; set; }
    }
}