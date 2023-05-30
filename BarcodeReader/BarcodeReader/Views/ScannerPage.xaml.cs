using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FluentFTP;
using Xamarin.Essentials;
using System.IO;
using BarcodeReader.Models;
using System.Threading;

namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        bool ScanAvailable = true;
        Documents CurrentDoc { get; set; }

        ObservableCollection<Barcodes> BarcodeList;
        public ScannerPage(ObservableCollection<Barcodes> BarcodeList, Documents currentdoc)
        {
            InitializeComponent();
            CurrentDoc = currentdoc;
            this.BarcodeList = BarcodeList;
        }

        void Scanhandler(ZXing.Result result)
        {
            if (ScanAvailable)
            {
                ScanAvailable = false;

                Device.BeginInvokeOnMainThread(() =>
                {

                    if (result.Text.Length < 13)
                    {
                        return;
                    }
                    lblResult.Text = result.Text;
                    DependencyService.Get<IAudio>().PlayAudioFile("scan.mp3");
                    Barcodes bar = new Barcodes { Code = result.Text, docId = CurrentDoc.Id };
                    BarcodeList.Add(bar);
                    bar.Id = App.Database.CreateBar(bar);

                });
                Thread.Sleep(2000);
                ScanAvailable = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }
        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            return;

        }
    }
}