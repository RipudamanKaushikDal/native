using MangaScrapeLib;
using MvvmHelpers;
using Reardo.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Reardo.ViewModels
{
    public class ComicPageModel 
    {
       

        public ComicPageModel()
        {
            return;
        }
        public ObservableRangeCollection<ChapterPages> ChapterImages { get; set; }


        public  ComicPageModel(IChapter chapter)
        {

       
            ChapterImages = new ObservableRangeCollection<ChapterPages>();
            var pagelist = Task.Run(async () => await GetPages(chapter)).Result;
            foreach (var page in pagelist)
            {
                  Task.Run(async () => await GetImages(page));
            }
        }

           

        public async Task<IReadOnlyList<IPage>> GetPages(IChapter chapter)
        {
            var pages = await chapter.GetPagesAsync();
            return pages;
        }

        public async Task GetImages(IPage page)
        {
            var image = await page.GetImageAsync();
            ImageSource img = ImageSource.FromStream(() => new MemoryStream(image));
            ChapterImages.Add(new ChapterPages() { PageImage = img });
        }



    }
}
