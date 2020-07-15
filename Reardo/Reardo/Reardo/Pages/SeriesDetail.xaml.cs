using MangaScrapeLib;
using Reardo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reardo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SeriesDetail : ContentPage
    {
        public SeriesDetail()
        {
            InitializeComponent();
        }


        
        public ISeries SelectedSeries { get; set; }

        public List<ChapterList> ChaptersDisplay = new List<ChapterList>();

        public SeriesDetail(ISeries seriesModel)
        {
            SelectedSeries = seriesModel;
           
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var totalChapters = await SelectedSeries.GetChaptersAsync();
            foreach (var chapter in totalChapters)
            {
                ChaptersDisplay.Add(new ChapterList() { ChapterName = chapter.Title, UpdatedDate = chapter.Updated });
            }

            
        }

        private void DisplayContent_Clicked(object sender, EventArgs e)
        {
            ChapterDisplay.ItemsSource = ChaptersDisplay;
        }
    }
}