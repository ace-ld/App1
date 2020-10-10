using Windows.UI.Xaml.Controls;
using CovidApi;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Windows.UI.Xaml;
using System.Text.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly HttpClient client = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Get_ClickAsync(object sender, RoutedEventArgs e)
        {
            progressBar.IsIndeterminate = true;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask = client.GetStreamAsync("https://api.covid19api.com/summary");

            var myDeserializedClass = await JsonSerializer.DeserializeAsync<Root>(await streamTask);

            foreach(var r in myDeserializedClass.Countries)
            {
                if(countryBoxSearch.Text == r.CountryStr)
                {
                    Country.Text = $"Country: {r.CountryStr}";
                    TotalDeaths.Text = $"Total deaths: {r.TotalDeaths}";
                    TotalConfirmed.Text = $"Total confirmed: {r.TotalConfirmed}";
                    TotalRecovered.Text = $"Total recovered: {r.TotalRecovered}";
                }else if (countryBoxSearch.Text == null)
                {
                    countryBoxSearch.Text = "Ooops!";
                }
            }
            progressBar.IsIndeterminate = false;
        }
    }
}
