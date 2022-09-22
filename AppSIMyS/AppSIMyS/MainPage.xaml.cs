using AppSIMyS.Fonts;
using AppSIMyS.Models;
using AppSIMyS.ViewModels;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using iText.Layout.Element;
using Xamarin.Essentials;

namespace AppSIMyS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage(string usuario)
        {
            
            InitializeComponent();
            LbUsuario.Text = usuario.Trim();
            BindingContext = new MainPageViewModel();
            MyCollectionView.SelectedItem = false;
            LblConexion.Text = "";
            //if (Connectivity.NetworkAccess!= NetworkAccess.Internet)
            //{
            //    LayoutConexion.BackgroundColor = Color.Red; 
            //    LblConexion.Text = "---- Sin conexión ----";
            //    DisplayAlert("Advertencia","Sin Internet","Ok");
            //}
            //else
            //{
            //    LayoutConexion.BackgroundColor = Color.Blue;
            //    LblConexion.Text = "---- Conectado ----";
            //    LblConexion.TextColor = Color.White;
            //    DisplayAlert("Advertencia", "Conectado", "Ok");
            //}
            //LlenarColletionView();
            

        }
        public async void LlenarColletionView()
        {
            //var stocksStartingWithA = db.Query<Clientes>("SELECT * FROM Items WHERE Symbol = ?", "A");
            //foreach (var s in stocksStartingWithA)
            //{
            //    Console.WriteLine("a " + s.Symbol);
            //}
            var GetLstcliente = await App.SQLiteDB.GetClsEmpresasAsync();

            if (GetLstcliente != null)
            {
                MyCollectionView.ItemsSource = GetLstcliente;
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //var cliente = e.Item as Clientes;
            DisplayAlert("Advertencia", "cliente.Rut", "Aceptar");
        }

        //private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var cliente = e.CurrentSelection;
        //    DisplayAlert("Advertencia", "cliente", "Aceptar");            
        //}

        //async void ExecuteHubTappedCommand(object parameter)
        //{
        //    await DisplayAlert("Message", "item " + parameter + " clicked", "Ok");
        //}

        private  void MyCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string clientesAnt = (e.PreviousSelection.FirstOrDefault() as Empresas)?.Empresa;
            string clientesAct = (e.CurrentSelection.FirstOrDefault() as Empresas)?.Empresa;
            string RutCliente = (e.CurrentSelection.FirstOrDefault() as Empresas)?.Rut;
            ImageSource LogoEmpresa = (e.CurrentSelection.FirstOrDefault() as Empresas)?.Logo;
            if (RutCliente!=null)
            {

                MyCollectionView.SelectedItem = false;
                 Navigation.PushAsync(new FormatoServicio(LbUsuario.Text, RutCliente, LogoEmpresa));
            }

           // await DisplayAlert("Registro", "Guardado Existosamente", "OK");

            //var GetLstcliente = await App.SQLiteDB.GetClsEmpresasAsync();

            //if (GetLstcliente != null)
            //{
            //    MyCollectionView.ItemsSource = GetLstcliente;
            //}
            //DisplayAlert("Advertencia", "Cliente Ant:" + clientesAnt + (char)13 + "Cliente Act:" +clientesAct, "Aceptar");



            //FormatoServicio formatoServicio = new FormatoServicio(LbUsuario.Text);
            //Navigation.PushAsync(formatoServicio);
            //DisplayAlert("Advertencia", clientesAct, "Aceptar");

        }
    }
}
