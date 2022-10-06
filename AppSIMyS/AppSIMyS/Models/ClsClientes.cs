using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSIMyS.Models
{
    public  class ClsClientes
    {
        [PrimaryKey]
        public string Rut { get; set; }
        public string Empresa { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public byte[]? Logo { get; set; }
    }
}
