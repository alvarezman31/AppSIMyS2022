using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSIMyS.Models
{
    public class ClsUsuarios
    {
        public string? password { get; set; }
        public string? rut { get; set; }
        public string? verifi { get; set; }
        public string? nombres { get; set; }
        public string? Cargo { get; set; }
        [PrimaryKey]
        public string? email { get; set; }
        public string? tipo { get; set; }
        public string? campo1 { get; set; }
        public string? campo2 { get; set; }
        public string? campo3 { get; set; }
        public string? campo4 { get; set; }
        public string? campo5 { get; set; }
        public string? cod_us_in { get; set; }
        public DateTime fec_in { get; set; }
        public string? cod_us_de { get; set; }
        public string? cod_us_up { get; set; }
        public string? cliente { get; set; }
        public string? Keys { get; set; }
        public int Verificado { get; set; }
        public string? Telefono { get; set; }
    }
}
