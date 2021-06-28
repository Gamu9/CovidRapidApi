using System;
using System.Collections.Generic;
using System.Text;

namespace CovidRapidApi.Domain.Models
{
    public class CountryValues
    {
        public string Continent { get; set; }
        public string Country { get; set; }
        public int? NewCases { get; set; }
        public int? ActiveCases { get; set; }
        public int? Deaths { get; set; }
    }
}
