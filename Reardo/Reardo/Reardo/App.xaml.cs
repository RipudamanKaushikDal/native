using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reardo
{
    public partial class App : Application
    {
        public static string DbPath; 
        public App()
        {
            InitializeComponent();
             

            MainPage = new MainPage();
        }

        public App(string path)
        {
            InitializeComponent();
            MainPage = new MainPage();
            DbPath = path;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
