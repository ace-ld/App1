using API;
using CovidApi;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        private static Root repo;
        private readonly List<string> CountryList = new List<string>();

        public MainPage()
        {
            this.InitializeComponent();
            GetStat();
        }

        private async void GetStat()
        {
            try
            {
                repo = await GetCovid.RequestApi();
                if(repo.Countries != null)
                {
                    foreach (var c in repo.Countries)
                        CountryList.Add(c.CountryStr);
                }
                else
                {
                    NewDeaths.Text = "Из API не пришло статистики :(";
                }
                
            }
            catch (HttpRequestException e)
            {
                NewDeaths.Text = e.Message;
            }

            progressBar.IsIndeterminate = false;
        }

        private void CountryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
                    DateCovid.Text = $"Данные на момент: {r.Date}";
                }
            }
        }

        // lock change window size
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size(600, 920));
        }
    }
}