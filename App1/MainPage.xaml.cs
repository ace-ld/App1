using API;
using System.Collections.Generic;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
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
                    NewDeaths.Text = $"Новые смерти: {r.NewDeaths}";
                    NewConfirmed.Text = $"Новые заражения: {r.NewConfirmed}";
                    NewRecovered.Text = $"Вылечилось: {r.NewRecovered}";
                    TotalDeaths.Text = $"Всего смертей: {r.TotalDeaths}";
                    TotalConfirmed.Text = $"Всего заразилось: {r.TotalConfirmed}";
                    TotalRecovered.Text = $"Всего вылечилось: {r.TotalRecovered}";
                    DateCovid.Text = $"Информация на момент: {r.Date}";
                }
            }
            progressBar.IsIndeterminate = false;
        }

        // lock change window size
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size(600, 920));
        }
    }
}
