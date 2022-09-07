using AppSIMyS.Models;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using AppSIMyS.Data;



namespace AppSIMyS.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Clientes> LClientes { get; set; }
        
        public MainPageViewModel()
        {

            

            string sql = "SELECT * FROM serv_clientes order by empresa ";
            //sql = "SELECT * FROM clientes LEFT join appser_firmas on rut=idservicio order by empresa ";
            LClientes = new ObservableCollection<Clientes>();

            using (MySqlConnection con = new MySqlConnection(Conectar.cCon))
            {
                con.Open();
                string a;
                using (MySqlCommand comando = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // ImageSource re1 = ImageSource.FromStream(() => new MemoryStream(Convert.ToByte(reader["imagen"].ToString())));
                            Image image1 = new Image();
                            Byte[] bindata;
                            //string firma = reader["firma"].ToString();
                            //MySqlCommand Sel = new MySqlCommand("select imgcolumn from tabla where campo1 = '" + _picker.SelectedItem.ToString() + "' Order by campo2", cConn.BD);
                            //if (reader["firma"].ToString().Trim() == "")
                            //{
                            bindata = (byte[])(reader["imagen"]);
                            //}
                            //else
                            //{
                            //    bindata = (byte[])(reader["firma"]);
                            //}
                            image1.Source = ImageSource.FromStream(() => new MemoryStream(bindata));
                            a = reader["rut"].ToString().Trim();
                            Clientes clientes = new Clientes()
                            {


                                Rut = reader["rut"].ToString().Trim(),// + '-' + reader["verifi"].ToString(),
                                Descripcion = reader["descripcion"].ToString().Trim(),
                                Empresa = reader["empresa"].ToString().Trim(),
                                Telefono = "Tel. " + reader["telefono"].ToString().Trim(),
                                Url = "Url. " + reader["url"].ToString().Trim(),
                                Direccion = "Dir. " + (reader["direccion"].ToString().Trim() + "                                                                  ").Substring(0, 50).Trim()
                                //Logo = image1.Source
                            };

                            LClientes.Add(clientes);

                        }
                    }
                }
                con.Close();
               
            }
        }
    }
}
