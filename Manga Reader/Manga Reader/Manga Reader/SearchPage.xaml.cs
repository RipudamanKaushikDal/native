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

        
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel();

        }

        private void MangaSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            var ViewModel = new SearchViewModel();
            ViewModel.SearchSeries.Execute(MangaSearch.Text);
        }
    }
}