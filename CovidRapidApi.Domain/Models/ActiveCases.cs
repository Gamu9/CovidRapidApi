using Newtonsoft.Json;

namespace CovidRapidApi.Domain.Models
{
    public class ActiveCases
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "percent")]
        public int Percent { get; set; }
    }
}