using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSIMyS.Models
{
    public class ImagenServicio
    {

        public int Id { get; set; }
        public string? Empresa { get; set; }
        public string? Comentario { get; set; }
        public ImageSource? Imagen { get; set; }
    }
     
}
