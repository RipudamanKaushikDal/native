using MangaScrapeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaReader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public List<SearchViewModel> searchResults = new List<SearchViewModel>();

        public SearchPage()
        {
            InitializeComponent();
        }
        private async void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var  repo = Repositories.AllRepositories[2];
            var series = await repo.SearchSeriesAsync(searchBar.Text);
            foreach (var item in series)
            {
                searchResults.Add(new SearchViewModel {Name=item.Title, Cover=item.CoverImageUri, Author=item.Author });
            }
            searchList.ItemsSource = searchResults;
        }
    }
}