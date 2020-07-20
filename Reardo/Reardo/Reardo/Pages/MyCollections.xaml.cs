using AngleSharp.Common;
using Reardo.Models;
using SQLite;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reardo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCollections : ContentPage
    {
        public ObservableCollection<Favorites> FavoritesList { get; set; } = new ObservableCollection<Favorites>();
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
                foreach (var series in seriesList)
                {
                    FavoritesList.Add(new Favorites() { SeriesTitle = series.SeriesTitle, CoverImage = series.CoverImage, SeriesUri = series.SeriesUri });
                }
                
                FavoritesDisplay.ItemsSource = FavoritesList;
            }
        }


    }
}