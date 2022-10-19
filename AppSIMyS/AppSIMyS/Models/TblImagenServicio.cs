using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSIMyS.Models
{
    public class TblImagenServicio
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; } 
        public int NroServicio { get; set; } 
        public string? Empresa { get; set; }
        public string? Comentario { get; set; }
        public byte[]? Imagen { get; set; }
    }
}
