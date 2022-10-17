using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSIMyS.Models
{
    public class TblServicios
    {
        [PrimaryKey]
        public string? Codigo{ get; set; }
        public string? Descripcion { get; set; }
        
    }
}
