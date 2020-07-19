using MangaScrapeLib;
using Reardo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Reardo.ViewModels
{
    public class ComicPageModel 
    {


        public ComicPageModel()
        {
            return;
        }
        public ObservableCollection<ChapterPages> ChapterImages { get; set; } = new ObservableCollection<ChapterPages>();

        public Command DownloadPages { get; set; }
        public ComicPageModel(IChapter chapter)
        {
            DownloadPages = new Command(async () =>
            {
                var pagelist = await chapter.GetPagesAsync();
                foreach (var page in pagelist)
                {
                    var imagebytes = await page.GetImageAsync();
                    var img = StreamImageSource.FromStream(() => new MemoryStream(imagebytes));
                    ChapterImages.Add(new ChapterPages() { PageImage = img });
                }
            });
 
        }
            

        
    }
}
