using MangaScrapeLib;
using Reardo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
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
        private Uri seriesCover;
      

        public string ChapterCount
        {
            get => $"Total Chapters:{chaptercount}"; set
            {
                chaptercount = value;
                OnPropertyChanged(nameof(ChapterCount));
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

        public ChapterViewModel()
        {

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
                     ChaptersList.Add(new ChapterList() { ChapterName = chapter.Title, UpdatedDate = chapter.Updated });
                 }

             });

        }




    }
}
