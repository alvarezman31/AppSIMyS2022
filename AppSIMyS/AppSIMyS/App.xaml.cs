using AppSIMyS.Services;
using AppSIMyS.Data;
using AppSIMyS.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace AppSIMyS
{
    public partial class App : Application
    {

        static SQLiteHelper db;
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Login.Login());
        }

        public static SQLiteHelper SQLiteDB
        {
            get { 
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "condominios.db3"));
                }
                return db; 
            }
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
