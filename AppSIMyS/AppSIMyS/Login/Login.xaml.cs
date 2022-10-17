using AppSIMyS.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace AppSIMyS.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        //private static byte[] _salt = __To_Do__("Add a app specific salt here");
        //public static string EncryptionKey = "WLN@2020";
        //static string strKey = "WLN@2020";
        //static string strIV = "101712";
        static int validado;
        public Login()
        {
            InitializeComponent();
            //TxtUsuario.Text= Sha256encrypt("a9zYz1ipnJ9i");

            if (validado==0 || validado == null)
            {
                string mensaje = "Aela.2018";

                string iv = "101712";
                string password = "WLN@2020";

                iv = MD5Hash(iv).Substring(0, 16);

                password = MD5Hash(password);


                //  iv = SHA256Hash(iv).Substring(0, 16);
                // password = SHA256Hash(password);

                string reta = Seguridad.EncryptString(mensaje, password, iv);


                string retb = Seguridad.Decrypt(reta, password, iv); //, "4ac4eefd21476f3351dc6dc335382764c281a99a21ac8ab69dd67db2d1c35f27", "25d4c3609e2cd59d");
                TxtUsuario.Focus();
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    LayoutConexion.BackgroundColor = Color.Red;
                    LblConexion.Text = "---- Sin conexión ----";
                    //DisplayAlert("Advertencia", "Sin Internet", "Ok");
                }
                else
                {
                    LayoutConexion.BackgroundColor = Color.Blue;
                    LblConexion.Text = "---- Conectado ----";
                    LblConexion.TextColor = Color.White;
                    //DisplayAlert("Advertencia", "Conectado", "Ok");

                }

                validado = 1;
            }
            
        }




        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider(); // SHA256CryptoServiceProvider();

            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public static string SHA256Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
           
            SHA256CryptoServiceProvider md5provider = new SHA256CryptoServiceProvider(); // SHA256CryptoServiceProvider();
            
            
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));            
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }            
            return hash.ToString();
        }

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            string usuario = Conectar.AccederSql(TxtUsuario.Text, TxtClave.Text);
            if (usuario == "")
            {
                DisplayAlert("Advertencia", "Usuario o Clave Incorrecta", "Aceptar");
            }
            else
            {
                Navigation.PushAsync(new MainPage(usuario));
            }
        }


        private void BtnEntrar_Pressed(object sender, EventArgs e)
        {
          //  Navigation.RemovePage(this);
        }
        //
        /*
         public static function encriptar($string){
			$output=FALSE;
			$key=hash('sha256', SECRET_KEY);
			$iv=substr(hash('sha256', SECRET_IV), 0, 16);
			$output=openssl_encrypt($string, METHOD, $key, 0, $iv);
			$output=base64_encode($output);
            return $output;
		}
		public static function desencriptar($string){
			$key=hash('sha256', SECRET_KEY);
			$iv=substr(hash('sha256', SECRET_IV), 0, 16);
			$output=openssl_decrypt(base64_decode($string), METHOD, $key, 0, $iv);
			return $output;
		} 
          
         
         */

        


        //
    }

}