using MangaScrapeLib;
using Reardo.Models;
using System.Collections.ObjectModel;
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
        public ObservableCollection<ChapterPages> ChapterImages { get; set; } = new ObservableCollection<ChapterPages>();

       
        public  ComicPageModel(IChapter chapter)
        {
           
            GetImages(chapter);
         
 
        }


        private async Task GetImages(IChapter chapter)
        {
            var pagelist = await chapter.GetPagesAsync();
            foreach (var page in pagelist)
            {
                var imagebytes = await page.GetImageAsync();
                ImageSource img = ImageSource.FromStream(() => new MemoryStream(imagebytes));
                ChapterImages.Add(new ChapterPages() { PageImage = img });


             
            }
        }
            

        
    }
}
