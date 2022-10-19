using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSIMyS.Models
{
    public class TblArchivoServicios
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public int NroServicio { get; set; }
        public string? Extension { get; set; }
        public string? Nombre { get; set; }
        public string? Mime { get; set; }
        public string? Archivo { get; set; }
        public byte[]? Archivo2 { get; set; }
    }
}
