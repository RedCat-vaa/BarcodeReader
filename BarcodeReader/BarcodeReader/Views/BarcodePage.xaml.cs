using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BarcodeReader.Models;
using System.IO;
using FluentFTP;


namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodePage : ContentPage
    {
        Documents CurrentDoc { get; set; }
        public ObservableCollection<Barcodes> BarcodeList { get; set; }
        public BarcodePage(int id)
        {
            CurrentDoc = new Documents() { Id = id };
            BarcodeList = new ObservableCollection<Barcodes>();
            InitializeComponent();
            this.BindingContext = this;
        }

        void UpdateList()
        {
             BarcodeList.Clear();

            var listbars = App.Database.GetBarcodes(CurrentDoc.Id);

            foreach (Barcodes br in listbars)
            {
                BarcodeList.Add(br);
            }
        }

        public BarcodePage(Documents doc)
        {
            CurrentDoc = doc;
            BarcodeList = new ObservableCollection<Barcodes>();
            InitializeComponent();
            this.BindingContext = this;
            UpdateList();
        }

        protected override void OnAppearing()
        {
            UpdateList();
            base.OnAppearing();
        }


        private async void ScanBarcode(object sender, EventArgs e)
        {
            ScannerPage scannerPage = new ScannerPage(BarcodeList, CurrentDoc);
            await Navigation.PushAsync(scannerPage);
        }
        private void DeleteBarcode(object sender, EventArgs e)
        {
            Barcodes br = ListBarcode.SelectedItem as Barcodes;
            if (br != null)
            {
                App.Database.DeleteBar(br.Id);
                BarcodeList.Remove(br);
            }
            base.OnAppearing();
        }

        private async void SendBarcode(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }

}