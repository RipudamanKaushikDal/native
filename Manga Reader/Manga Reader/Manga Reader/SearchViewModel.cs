using MangaScrapeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using ICommand = System.Windows.Input.ICommand;

namespace MangaReader
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        

        public string SearchWord
        {
            get { return search; }
            private set
            {
                if (search != value )
                {
                    search = value;
                    OnPropertyChanged("SearchWord");
                }
            }
        }

        public List<DisplaySearch> DisplayData = new List<DisplaySearch>();

        List<IChapter> selectedSeries;
        string search;
        public event PropertyChangedEventHandler PropertyChanged;

        public List<IChapter> SelectedSeries { get { return selectedSeries; }
            private set {
                if (selectedSeries != value)
                {
                    selectedSeries = value;
                    OnPropertyChanged("SelectedSeries");
                }
            }
        }

        public ICommand DownloadChapter { get; private set; }

        public ICommand SearchSeries { get; private set; }
        public SearchViewModel()
        {
            SearchSeries = new Command<string>(async (text) => await DownloadSeriesAsync(text));
            DownloadChapter = new Command<string>(async (x) => await GetChapters(x));
        
        }

        
        private async Task GetChapters (string value)
        {
            var repo = Repositories.AllRepositories[2];
            var series = await repo.SearchSeriesAsync(value);
            var chapters = await series[0].GetChaptersAsync();
            selectedSeries = chapters.ToList();

        }

        private async Task DownloadSeriesAsync (string text)
        {
            var repo = Repositories.AllRepositories[2];
            var series = await repo.SearchSeriesAsync(text);

            foreach (var item in series)
            {
                DisplayData.Add( new DisplaySearch(){ Name = item.Title, Cover = item.CoverImageUri, Author = item.Author});
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
