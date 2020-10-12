using API;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        private readonly List<string> CountryList = new List<string>();

        public MainPage()
        {
            this.InitializeComponent();
            GetCountry();
        }

        private async void GetCountry()
        {
            var repo = await GetCovid.RequestApi();
            foreach (var c in repo.Countries)
                CountryList.Add(c.CountryStr);
        }

        private async void CountryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            progressBar.IsIndeterminate = true;

            var repo = await GetCovid.RequestApi();
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
