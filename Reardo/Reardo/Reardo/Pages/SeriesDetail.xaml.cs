using Reardo.Models;
using Reardo.ViewModels;
using System.Linq;

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

        private async void ChapterDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = (e.CurrentSelection.FirstOrDefault() as ChapterList);
            var chapter = selection.ChapterModel;
            Progress.Text = (chapter.ReadingOrder+1).ToString();
            var comicspage = new ComicPages()
            {
                BindingContext = new ComicPageModel(chapter)
            };

;           await Navigation.PushAsync(comicspage);
        }
    }
}