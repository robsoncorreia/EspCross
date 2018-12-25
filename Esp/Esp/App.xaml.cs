using Esp.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Esp
{
    public partial class App : Application
    {
        private static ComandoDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static ComandoDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ComandoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ComandoSQLite.db3"));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}