using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BarcodeReader.Views;

namespace BarcodeReader
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ToSettingsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }

        private async void ToDocumentPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Document());
        }

    }
}
