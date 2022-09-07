using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSIMyS.Models
{
    public class ClsDetServicios
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int IdServicio{ get; set; }
        public int IdTipoServicio{ get; set; }
        public string Observacion { get; set; }

    }
}
