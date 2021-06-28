using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CovidRapidApi.Domain.Models
{
    public class StatisticsValues
    {
        [JsonProperty(PropertyName = "get")]
        public string Get { get; set; }
        [JsonProperty(PropertyName = "parameters")]
        public List<object> Parameters { get; set; }
        [JsonProperty(PropertyName = "errors")]
        public List<object> Errors { get; set; }
        [JsonProperty(PropertyName = "results")]
        public int? Results { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<Response> Response { get; set; }
    }
}
