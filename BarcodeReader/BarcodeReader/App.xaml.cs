using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BarcodeReader.Models;
using System.IO;

namespace BarcodeReader
{
    public partial class App : Application
    {

        public const string DATABASE_NAME = "Barcodes.db";
        public static DB database;
        public static DB Database
        {
            get
            {
                if (database == null)
                {
                    database = new DB(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
