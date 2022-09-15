using AppSIMyS.Services;
using AppSIMyS.Data;
using AppSIMyS.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using AppSIMyS.Models;
using MySqlConnector;
using System.Data.SqlClient;

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

        protected  override void OnStart()
        {
            base.OnStart();
             //App.SQLiteDB.DeleteEmpresas();
             //App.SQLiteDB.DeleteEmpresas();
             App.SQLiteDB.EliminarClsEmpresas();
            //Limpiar = 1;
            string comando = "Select * from conds_empresas order by descripcion";
            SqlConnection con1 = Conectar.conectarSql();
            SqlDataReader res1 = Conectar.consultarSql(comando, con1);
            //ClsEmpresas empresa = new ClsEmpresas();
            ClsEmpresas empresaNew = new ClsEmpresas();
            while (res1.Read())
            {
                empresaNew.Rut = res1["rut"].ToString();
                empresaNew.Empresa = res1["empresa"].ToString();
                empresaNew.Descripcion = res1["descripcion"].ToString();
                empresaNew.Direccion = res1["direccion"].ToString();
                empresaNew.Telefono = res1["telefono"].ToString();
                empresaNew.Logo = (byte[])res1["logo"]; //ImageSource.FromStream(() => new MemoryStream((byte[])res1["logo"]));// 

                //if (App.SQLiteDB.GetClsEmpresasByRutAsync(empresaNew.Rut) != null)
                //{

                //    App.SQLiteDB.UpdateEmpresa(empresaNew);
                //}
                //else
                //{
                    //App.SQLiteDB.SaveEmpresa(empresaNew);
                   // App.SQLiteDB.SaveEmpresa(empresaNew);
                   App.SQLiteDB.AgregarClsEmpresas(empresaNew);
                //}
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
