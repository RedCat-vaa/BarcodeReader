using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BarcodeReader.ViewModels;
using Xamarin.Essentials;

namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        private SettingsVM settingsVM;
        public Settings()
        {
            InitializeComponent();

            settingsVM = new SettingsVM();
            this.BindingContext = settingsVM;
            settingsVM.Server = Preferences.Get("Server", "");
            settingsVM.User = Preferences.Get("User", "");
            settingsVM.Password = Preferences.Get("Password", "");
        }

        private async void SaveSettings(object sender, EventArgs e)
        {
            Preferences.Set("Server", settingsVM.Server);
            Preferences.Set("User", settingsVM.User);
            Preferences.Set("Password", settingsVM.Password);
            await Navigation.PopAsync();
        }
    }
}