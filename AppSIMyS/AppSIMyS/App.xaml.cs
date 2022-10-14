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
using Xamarin.Essentials;

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

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                /*LayoutConexion.BackgroundColor = Color.Red;
                LblConexion.Text = "---- Sin conexión ----";
                DisplayAlert("Advertencia", "Sin Internet", "Ok");*/

            }
            else
            {
                App.SQLiteDB.EliminarClsEmpresas();
                App.SQLiteDB.EliminarClsClientes();
                App.SQLiteDB.EliminarTblUsuarios();
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
                    empresaNew.Email= res1["email"].ToString();
                    empresaNew.Logo = null;// res1["logo"]; //ImageSource.FromStream(() => new MemoryStream((byte[])res1["logo"]));// 
                    empresaNew.PiePagina = null;// res1["PiePagina"]; //ImageSource.FromStream(() => new MemoryStream((byte[])res1["lo
                    
                    if (res1["logo"].ToString() != "")
                        empresaNew.Logo = (byte[])res1["logo"]; //ImageSource.FromStream(() => new MemoryStream((byte[])res1["logo"]));// 
                    if (res1["PiePagina"].ToString() != "")
                        empresaNew.PiePagina = (byte[])res1["PiePagina"]; //ImageSource.FromStream(() => new MemoryStream((byte[])res1["logo"]));// 
                    App.SQLiteDB.AgregarClsEmpresas(empresaNew);

                }
                comando = "Select * from conds_clientes order by razon";
                //con1 = Conectar.conectarSql();
                res1.Close();
                res1 = Conectar.consultarSql(comando, con1);
                while (res1.Read())
                {
                    ClsClientes clienteNew = new ClsClientes();
                    {

                        clienteNew.Rut = res1["rut"].ToString().Trim() + "-" + res1["verifi"].ToString().Trim();
                        clienteNew.Empresa = res1["rut"].ToString();
                        clienteNew.Descripcion = res1["razon"].ToString();
                        clienteNew.Direccion = res1["direccion"].ToString();
                        clienteNew.Telefono = res1["telefono"].ToString();
                        clienteNew.Url = res1["url"].ToString();
                        clienteNew.Email = res1["email"].ToString();
                        clienteNew.Logo = null;// res1["logo"]; //ImageSource.FromStream(() => new MemoryStream((byte[])res1["logo"]));// 
                        if (res1["logo"].ToString() != "")
                            clienteNew.Logo = (byte[])res1["logo"]; //ImageSource.FromStream(() => new MemoryStream((byte[])res1["logo"]));// 
                        App.SQLiteDB.AgregarClsClientes(clienteNew);
                    }
                }
                comando = "Select * from usuarios ";                
                res1.Close();
                res1 = Conectar.consultarSql(comando, con1);
                while (res1.Read())
                {
                    TblUsuarios clienteNew = new TblUsuarios();
                    {

                        clienteNew.rut = res1["rut"].ToString().Trim();// + "-" + res1["verifi"].ToString().Trim();
                        clienteNew.nombres = res1["nombres"].ToString();
                        clienteNew.email = res1["email"].ToString();
                        clienteNew.password = res1["password"].ToString();
                        App.SQLiteDB.AgregarTblUsuarios(clienteNew);
                    }
                }
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
