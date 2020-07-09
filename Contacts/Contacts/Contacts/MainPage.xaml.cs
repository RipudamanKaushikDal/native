using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contacts
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void saveButton_Clicked(object sender, EventArgs e)
        {
            Contact contact = new Contact()
            {
                Name=nameEntry.Text,
                Phone= phoneEntry.Text,
                Email = emailEntry.Text

            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Contact>();
                int rowsAdded = conn.Insert(contact);
            }
            
        }
    }
}
