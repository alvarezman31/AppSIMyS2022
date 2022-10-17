
using AppSIMyS.Models;
using AppSIMyS.ViewModels;
using AppSIMyS.Menu;
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
using AppSIMyS.Services;

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

        //async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        //{
        //    (sender as Button).IsEnabled = false;

        //    Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
        //    if (stream != null)
        //    {
        //        image.Source = ImageSource.FromStream(() => stream);
        //    }

        //  (sender as Button).IsEnabled = true;
        //}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //var cliente = e.Item as Clientes;
           // DisplayAlert("Advertencia", "cliente.Rut", "Aceptar");
        }


        private  void MyCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string clientesAnt = (e.PreviousSelection.FirstOrDefault() as Empresas)?.Empresa;
            string clientesAct = (e.CurrentSelection.FirstOrDefault() as Empresas)?.Empresa;
            string RutCliente = (e.CurrentSelection.FirstOrDefault() as Empresas)?.Rut;
            ImageSource LogoEmpresa = (e.CurrentSelection.FirstOrDefault() as Empresas)?.Logo;
            if (RutCliente!=null)
            {

                MyCollectionView.SelectedItem = false;
                 //Navigation.PushAsync(new FormatoServicio(LbUsuario.Text, RutCliente, LogoEmpresa));
                 Navigation.PushAsync(new MenuTipoInforme(LbUsuario.Text, RutCliente, LogoEmpresa));
            }

        }
    }
}
