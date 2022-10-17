using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSIMyS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSIMyS.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuTipoInforme : ContentPage
    {
        public string usuario1;
        public string RutCliente1;
        public ImageSource Logo1;
        public MenuTipoInforme(string usuario, string RutCliente, ImageSource LogoEmpresa)
        {
            InitializeComponent();
            usuario1 = usuario;
            RutCliente1 = RutCliente;
            Logo1 = LogoEmpresa;
        }

        private void BtnMantencion_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnServicios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormatoServicio(usuario1, RutCliente1));
        }
    }
}