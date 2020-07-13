using MangaScrapeLib;
using Reardo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reardo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public List<SearchList> SearchResults = new List<SearchList>();

        private async void ComicSearch_Pressed(object sender, EventArgs e)
        {
            var repo = Repositories.AllRepositories[2];
            var serieslist = await repo.SearchSeriesAsync(ComicSearch.Text);
            foreach (var series in serieslist)
            {
                SearchResults.Add(new SearchList() { Name = series.Title, Author = series.Author, 
                                                      Cover = series.CoverImageUri, });
            }
            SearchDisplay.ItemsSource = SearchResults;
        }

        private void SearchDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSeries = e.CurrentSelection;
            string msg = String.Empty;
            for (int i =0; i<selectedSeries.Count; i++)
            {
                var fields = selectedSeries[i] as SearchList;
                msg = $"{fields.Name} is selected";
            }

            DisplayAlert("Selected", msg, "OK");
        }
    }
}