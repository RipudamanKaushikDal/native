﻿using MangaScrapeLib;
using Reardo.Models;
using Reardo.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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

        public ObservableCollection<SearchList> SearchResults = new ObservableCollection<SearchList>();
        
        public int RepoNumber { get; set; } = 2;

        private async void ComicSearch_Pressed(object sender, EventArgs e)
        {
            var repo = Repositories.AllRepositories[RepoNumber];
            var serieslist = await repo.SearchSeriesAsync(ComicSearch.Text);
            foreach (var series in serieslist)
            {
                SearchResults.Add(new SearchList() { Name = series.Title, Author = series.Author, 
                                                      Cover = series.CoverImageUri, SeriesModel=series });
            }
            SearchDisplay.ItemsSource = SearchResults;
        }

        private async void SearchDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = (e.CurrentSelection.FirstOrDefault() as SearchList);
            ISeries selectedSeries = selection.SeriesModel;
            var seriesdetail = new SeriesDetail();
            var chapterview = new ChapterViewModel(selectedSeries);
            seriesdetail.BindingContext = chapterview;

            await Navigation.PushAsync(seriesdetail);
            SearchResults.Clear();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SearchResults.Clear();
            ComicSearch.Text = String.Empty;
        }

  
        private void ComicSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == String.Empty)
            {
                SearchResults.Clear();
            }
        }
    }
}