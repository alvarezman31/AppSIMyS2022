using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace AppSIMyS.Models
{
    public class ClsServicios
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int NroServicio { get; set; }
        public string? Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string? Tecnico { get; set; }
        public string? Descripcion { get; set; }
        public string? Observacion { get; set; }
        //public byte[]? FirmaTecnico { get; set; }
        //public byte[]? FirmaCliente { get; set; }
    }
}
