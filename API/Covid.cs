using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CovidApi
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

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