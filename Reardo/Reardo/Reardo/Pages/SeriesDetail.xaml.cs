using MangaScrapeLib;
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

    [QueryProperty("SeriesTitle", "title")]
    [QueryProperty("SeriesUri", "seriesLink")]
    public partial class SeriesDetail : ContentPage
    {
        public SeriesDetail()
        {
            InitializeComponent();
        }

        string seriestitle;
        string seriesuri;
        public string SeriesTitle { get => seriestitle; set => seriestitle = Uri.UnescapeDataString(value); }

        public string SeriesUri { get => seriesuri; set => seriesuri = value; }

        public int ChapterCount { get; set; }
        public List<IChapter> ChaptersList { get; set; }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var Serieslink = new Uri(SeriesUri);
            var series = Repositories.GetSeriesFromData(Serieslink, SeriesTitle);
            var totalChapters =  await series.GetChaptersAsync();
            ChaptersList = totalChapters.ToList();
            ChapterCount = ChaptersList.Count;
            ChapterDisplay.ItemsSource = ChaptersList;
        }

    }
}