using AppSIMyS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AppSIMyS.ViewModels
{

    
    public class FormatoServicioViewModel
    {
        public static ObservableCollection<ImagenServicio> LstImagenes { get; set; }
        public FormatoServicioViewModel()
        {           
            string sql = "SELECT * FROM TblImagenServicio order by id";
            LstImagenes = new ObservableCollection<ImagenServicio>();
            var ImagenesServicios = App.SQLiteDB.GetTblImagenesByAsync2();
            LstImagenes.Clear();
            foreach (var item in ImagenesServicios)
            {
                Image image1 = new Image();
                Byte[] bindata;
                bindata = (byte[])(item.Imagen);
                image1.Source = ImageSource.FromStream(() => new MemoryStream(bindata));
                ImagenServicio imagen  = new ImagenServicio();
                imagen.Comentario = item.Comentario;
                imagen.Imagen = image1.Source;
                imagen.Id = item.Id;
                LstImagenes.Add(imagen);
            }          
        }
    }
}
