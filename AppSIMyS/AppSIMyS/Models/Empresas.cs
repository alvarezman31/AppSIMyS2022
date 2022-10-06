using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSIMyS.Models
{
    public class Empresas
    {
        public string Empresa { get; set; }
        public string Descripcion { get; set; }
        public string Rut { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public ImageSource? Logo { get; set; }
        public ImageSource? PiePagina { get; set; }

    }
}
