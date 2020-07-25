using AngleSharp.Text;
using MangaScrapeLib;
using Reardo.Models;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Reardo.ViewModels
{

    public class ChapterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ISeries SelectedSeries { get; set; }

        public ObservableCollection<ChapterList> ChaptersList { get; set; } = new ObservableCollection<ChapterList>();

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
            get => $" /{chaptercount}" ; set
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


        public Command DownloadChapters { get; set; }
        public Command AddSeries { get; set; }

        public ChapterViewModel()
        {

        }

        public ChapterViewModel(ISeries series, Uri cover, int seriesID, string progress)
        {
            SelectedSeries = series;
            Cover = cover;
            GetChapters();
            ReadProgress = progress;
            DatabaseIndicator = "Added";
            ShowTrack = "Track";
            DownloadChapters = new Command(() => SaveProgress(seriesID));
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

            DownloadChapters = new Command(async () =>
             {
                 var totalChapters = await SelectedSeries.GetChaptersAsync();
                 ChapterCount = totalChapters.Count.ToString();
                 foreach (var chapter in totalChapters)
                 {
                     ChaptersList.Add(new ChapterList() { ChapterName = chapter.Title, UpdatedDate = chapter.Updated, ChapterModel = chapter });
                 }

             });

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

        private async Task GetChapters()
        {
            var totalChapters = await SelectedSeries.GetChaptersAsync();
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
