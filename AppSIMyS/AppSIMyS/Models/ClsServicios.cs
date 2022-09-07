using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace AppSIMyS.Models
{
    public class ClsServicios
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string Tecnico { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }


    }
}
