using AngleSharp.Common;
using MangaScrapeLib;
using Reardo.Models;
using Reardo.ViewModels;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reardo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCollections : ContentPage
    {
       // public ObservableCollection<Favorites> FavoritesList { get; set; } = new ObservableCollection<Favorites>();
        public MyCollections()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.DbPath))
            {
                conn.CreateTable<Favorites>();
                var seriesList = conn.Table<Favorites>().ToList();
                
                FavoritesDisplay.ItemsSource = seriesList;
            }
        }

        private async void FavoritesDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = (e.CurrentSelection.FirstOrDefault() as Favorites);
            string Seriestitle = selection.SeriesTitle;
            Uri Serieslink = selection.SeriesUri;
            Uri CoverImage = selection.CoverImage;
            int objectID = selection.Id;
            string progress = selection.Progress;
            ISeries series = Repositories.GetSeriesFromData(Serieslink, Seriestitle);
            var seriesdetail = new SeriesDetail()
            {
                BindingContext = new ChapterViewModel(series, CoverImage, objectID, progress)
            };
       
            await Navigation.PushAsync(seriesdetail);
        }
    }
}