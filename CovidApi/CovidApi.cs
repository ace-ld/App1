using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CovidApi
{
    public struct Global
    {
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
    }

    public struct Country
    {
        [JsonPropertyName("Country")]
        public string CountryStr { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
    }

    public struct Root
    {
        public Global Global { get; set; }
        public List<Country> Countries { get; set; }
        public DateTime Date { get; set; }
    }
}
