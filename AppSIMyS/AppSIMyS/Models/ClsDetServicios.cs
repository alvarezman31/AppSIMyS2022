using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSIMyS.Models
{
    public class ClsDetServicios
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int NroServicio { get; set; }
        public int IdServicio{ get; set; }
        public int IdTipoServicio{ get; set; }
        public string Codigo { get; set; }
        public int Cantidad { get; set; }
        public string Comentario { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }

    }
}
