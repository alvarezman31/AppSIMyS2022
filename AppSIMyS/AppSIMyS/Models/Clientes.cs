using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSIMyS.Models
{
    public class Clientes
    {
        [PrimaryKey]
        public string Rut { get; set; }
        public string Empresa { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string Url { get; set; }
        public string Direccion { get; set; }
        public byte[]? Logo { get; set; }
    }
}
