using CovidApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        private readonly List<string> CountryList = new List<string>();
        private static readonly HttpClient client = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();
            GetCountry();
        }

        // запрос к апи
        private async Task<Root> RequestApi()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask = client.GetStreamAsync("https://api.covid19api.com/summary");
            var myDeserializedClass = await JsonSerializer.DeserializeAsync<Root>(await streamTask);

            return myDeserializedClass;
        }

        private async void GetCountry()
        {
            var repo = await RequestApi();
            foreach (var c in repo.Countries)
                CountryList.Add(c.CountryStr);
        }

        private async void CountryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            progressBar.IsIndeterminate = true;

            var repo = await RequestApi();
            string countryName = e.AddedItems[0].ToString();

            foreach (var r in repo.Countries)
            {
                if (countryName == r.CountryStr)
                {
                    NewDeaths.Text = $"New deaths: {r.NewDeaths}";
                    NewConfirmed.Text = $"New confirmed: {r.NewConfirmed}";
                    NewRecovered.Text = $"New recovered: {r.NewRecovered}";
                    TotalDeaths.Text = $"Total deaths: {r.TotalDeaths}";
                    TotalConfirmed.Text = $"Total confirmed: {r.TotalConfirmed}";
                    TotalRecovered.Text = $"Total recovered: {r.TotalRecovered}";
                    DateCovid.Text = r.Date.ToString();
                }
            }

            progressBar.IsIndeterminate = false;
        }
    }
}
