using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CovidApi
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task GetCovid()
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories.Countries)
            {
                Console.WriteLine($"Country: {repo.CountryStr}");
                Console.WriteLine($"Total deaths: {repo.TotalDeaths}");
                Console.WriteLine($"Total confirmed: {repo.TotalConfirmed}");
                Console.WriteLine($"Total recovered: {repo.TotalRecovered}");
                Console.WriteLine($"New deaths: {repo.NewDeaths}");
                Console.WriteLine($"New confirmed: {repo.NewConfirmed}");
                Console.WriteLine($"New recovered: {repo.NewRecovered}");
                Console.WriteLine($"Date: {repo.Date}");
                Console.WriteLine("========================");
            }
        }

        public static async Task<Root> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask = client.GetStreamAsync("https://api.covid19api.com/summary");

            var myDeserializedClass = await JsonSerializer.DeserializeAsync<Root>(await streamTask);
            return myDeserializedClass;
        }
    }
}