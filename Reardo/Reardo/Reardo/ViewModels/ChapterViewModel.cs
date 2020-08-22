using MangaScrapeLib;
using Reardo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Reardo.ViewModels
{

    public class ChapterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ISeries SelectedSeries { get; set; }

        public ObservableCollection<ChapterList> ChaptersList { get; set; }

        string chaptercount;
        string readProgress;
        private Uri seriesCover;
        private string databaseIndicator = "+ Add";
        private string showTrack = "Chapters";


        public string ShowTrack
        {
            get => showTrack;
            set
            {
                showTrack = value;
                OnPropertyChanged(nameof(ShowTrack));
            }
        }

        public string DatabaseIndicator
        {
            get => databaseIndicator; set
            {
                databaseIndicator = value;
                OnPropertyChanged(nameof(DatabaseIndicator));
            }
        }


        public string ChapterCount
        {
            get => $" / {chaptercount}" ; set
            {
                chaptercount = value;
                OnPropertyChanged(nameof(ChapterCount));
            }
        }

        public string ReadProgress
        {
            get => readProgress ; set
            {
                readProgress = value;
                OnPropertyChanged(nameof(ReadProgress));
            }
        }
        public Uri Cover
        {
            get => seriesCover; set
            {
                seriesCover = value;
                OnPropertyChanged(nameof(Cover));
            }
        }

      
        public ICommand DownloadChapters { get; set; }
        public ICommand AddSeries { get; set; }

        public ChapterViewModel()
        {

        }

        public ChapterViewModel(ISeries series, Uri cover, int seriesID, string progress)
        {
            SelectedSeries = series;
            Cover = cover;
            ChaptersList = new ObservableCollection<ChapterList>();
            var totalChapters = Task.Run(async () => await SelectedSeries.GetChaptersAsync()).Result;
            ReadProgress = progress;
            DatabaseIndicator = "Added";
            DownloadChapters = new Command(() => { 
                if (ShowTrack == "Track")
                {
                    SaveProgress(seriesID);
                }
                else
                {
                    GetChapters(totalChapters);
                    ShowTrack = "Track";
                }
                      
            });

            AddSeries = new Command(() => {
                if (DatabaseIndicator == "Added")
                {
                    RemoveFromDB(seriesID);
                }
                else
                {
                    AddtoDatabase();
                }
            });

        }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ChapterViewModel(ISeries series)
        {
            SelectedSeries = series;
            Cover = SelectedSeries.CoverImageUri;
            ChaptersList = new ObservableCollection<ChapterList>();
            var totalChapters = Task.Run(async () =>  await SelectedSeries.GetChaptersAsync()).Result;
            DownloadChapters = new Command(() => GetChapters(totalChapters));
                           
            AddSeries = new Command(() => AddtoDatabase());

        }

        private void AddtoDatabase()
        {
            string Title = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SelectedSeries.Title.ToLower());
            Favorites favoriteseries = new Favorites()
            {
                SeriesTitle = Title,
                CoverImage = SelectedSeries.CoverImageUri,
                SeriesUri = SelectedSeries.SeriesPageUri
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DbPath))
            {
                conn.CreateTable<Favorites>();
                int rows = conn.Insert(favoriteseries);
                
            }

            DatabaseIndicator = "Added";
        }

        private void RemoveFromDB(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DbPath))
            {
                conn.Delete<Favorites>(id);
                DatabaseIndicator = "+ Add";
            }
           
        }

         private void GetChapters(IReadOnlyList<IChapter> totalChapters)
        {
           
            ChapterCount = totalChapters.Count.ToString();
            foreach (var chapter in totalChapters)
            {
                ChaptersList.Add(new ChapterList() { ChapterName = chapter.Title, UpdatedDate = chapter.Updated, ChapterModel = chapter });
            }
        }

        private void SaveProgress(int seriesID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DbPath))
            {

                conn.Execute("UPDATE Favorites SET Progress = ? Where Id = ?", ReadProgress, seriesID);
            }
        }


    }
}
