using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using BarcodeReader.Models;
using FluentFTP;
using System.IO;

namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Document : ContentPage
    {

        public ObservableCollection<Documents> DocList { get; set; }
        public Document()
        {
            DocList = new ObservableCollection<Documents>();
            InitializeComponent();
            this.BindingContext = this;
        }

        private async void Create(object sender, EventArgs e)
        {
            Documents doc = new Documents();
            doc.Id = App.Database.CreateDoc(doc);
            BarcodePage brPage = new BarcodePage(doc.Id);
            await Navigation.PushAsync(brPage);
        }
        private void Delete(object sender, EventArgs e)
        {
            Documents doc = ListDocuments.SelectedItem as Documents;
            if (doc != null)
            {
                App.Database.DeleteDoc(doc.Id);
                DocList.Remove(doc);
            }

            base.OnAppearing();
        }

        private async void Edit(object sender, EventArgs e)
        {
            Documents doc = ListDocuments.SelectedItem as Documents;
            if (doc != null)
            {
                BarcodePage brPage = new BarcodePage(doc);
                await Navigation.PushAsync(brPage);
            }

        }

        private async void MyAlert(string message)
        {
            await DisplayAlert("Уведомление",message, "ОК");
        }

        protected override void OnAppearing()
        {
            try
            {
                DocList.Clear();
                var listdocs = App.Database.GetDocuments();
                foreach (Documents doc in listdocs)
                {
                    DocList.Add(doc);
                }
            }
            catch(Exception ex)
            {
                MyAlert(ex.Message);
            }
            
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void Send(object sender, EventArgs e)
        {

            Documents doc = ListDocuments.SelectedItem as Documents;
            if (doc != null)
            {
                var listbars = App.Database.GetBarcodes(doc.Id);

                if (listbars.Count() == 0)
                {
                    return;
                }

                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string filename = Convert.ToString(DateTime.Now).Replace(" ", "").Replace(".", "").Replace(":", "") + ".txt";
                string textcode = "";
                try
                {
                    var client = new FtpClient(Preferences.Get("Server", ""), Preferences.Get("User", ""), Preferences.Get("Password", ""));
                    client.Connect();

                    foreach (Barcodes br in listbars)
                    {
                        textcode += br.Code + ";";
                    }

                    // перезаписываем файл
                    File.WriteAllText(Path.Combine(folderPath, filename), textcode);

                    client.UploadFile(Path.Combine(folderPath, filename), "/" + filename);

                    File.Delete(Path.Combine(folderPath, filename));

                    client.Disconnect();
                    doc.IsSent = true;
                    App.Database.UpdateDoc(doc);
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                }

                await DisplayAlert("Уведомление", "Документ отправлен", "OK");
            }
        }
    }
}