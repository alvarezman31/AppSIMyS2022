using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using static System.Net.WebRequestMethods;
using Xamarin.Forms;

namespace AppSIMyS.Models
{
    public class Conectar
    {
        //public static string cCon = "Server=srv29.cpanelhost.cl;UserID=cal42270_wln;Database=cal42270_worldlan;Password=Wln.2020;";
        public static string cCon = "Server=alvaluc.com;UserID=amdappserv;Database=alvalucc_appserv;Password=M@@L+2209;";
        private static string Servidor = "alvaluc.com";// WebConfigurationManager.AppSettings["servidor"].ToString(); 
        private static string Usuario = "AdmCondo";// WebConfigurationManager.AppSettings["usuario"].ToString();
        private static string Base = "alvalucc_condominios"; // WebConfigurationManager.AppSettings["base"].ToString();
        private static string PassBD = "!1m19g3Ee";// clsSeguridad.Desencriptar2(WebConfigurationManager.AppSettings["passbd"].ToS
        public static string Acceder(string usuario, string clave)
        {
            clave = md5(clave);

            string sql = "SELECT * FROM serv_usuario  where usuario='" + usuario + "' and clave='" + clave + "'";

            using (MySqlConnection con = new MySqlConnection(cCon))
            {
                con.Open();

                using (MySqlCommand comando = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        sql = "";
                        while (reader.Read())
                        {
                            sql = reader["nombre"].ToString().Trim();// + '-' + reader["verifi"].ToString(),
                            sql = reader["usuario"].ToString().Trim();// + '-' + reader["verifi"].ToString(),
                        }
                    }
                }
                con.Close();
                return sql;
            }

        }

        public static string AccederSql(string usuario, string clave)
        {
            string consulta="";
            var ImagenesServicios = App.SQLiteDB.GetUsuario(usuario, Seguridad.Encriptar2(clave));            
            foreach (var item in ImagenesServicios)
            {
                consulta = item.rut;
            }
            return consulta;
            //    try
            //{
            //    //lbEstado.Text = "";
            //    consulta = "select * from usuarios where email = '" + usuario + "' and password= '" + Seguridad.Encriptar2(clave) + "'";
            //    // String consulta = "select * from usuarios where email = '" + TxtEmail + "' and password= '" + TxtPassword.Text + "'";

            //    SqlConnection con = conectarSql();
            //    SqlDataReader reader = consultarSql(consulta, con);  // cmd.ExecuteReader();
            //    consulta = "";
            //    if (reader.Read())
            //    {                 
            //        consulta = reader["rut"].ToString().Trim();// + '-' + reader["verifi"].ToString(),
            //    }
            //   con.Close();
            //}
            //catch (SqlException ex)
            //{
            //    //Disp(ex.Message);
            //    //Response.Redirect("mantener/Configuracion.aspx");
            //}
            //return consulta;
        }

        public static string md5(string Value)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Value);
            data = x.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();

            ret = ret.Replace('0', 'O').Replace('3', 'e').Replace('b', 'B');
            return ret;
        }

        public static int GuardarFirma(string  firma, int IdServicio )
        {
            int retorno;
            string sql = string.Format("insert into serv_firmas (IdServicio,firma) Values({0},'{1}')",IdServicio,firma);
            MySqlConnection con = new MySqlConnection(cCon);
            con.Open();
            MySqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = sql;
            try
            {
                retorno = cmd1.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                string m = ex.Message;
                retorno = 0;
            }
            con.Close();
            return retorno;
        }

        public static int GuardarServicio(ClsServicios servicios)
        {
            int retorno;
            string sql = string.Format("insert into serv_servicios (cliente,tecnico,descripcion) Values('{0}','{1}','{2}')", servicios.Cliente, servicios.Tecnico, servicios.Observacion);
            MySqlConnection con = new MySqlConnection(cCon);
            con.Open();
            MySqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = sql;
            try
            {
                retorno = cmd1.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                string m = ex.Message;
                retorno = 0;
            }
            con.Close();
            return retorno;
        }

        public static int GuardarDetalleServicio(ClsDetServicios servicios)
        {
            int retorno;
            string sql = string.Format("insert into serv_detservicios (idservicio, idtiposervicio, observacion) Values('{0}','{1}','{2}')", servicios.IdServicio, servicios.IdTipoServicio, servicios.Observacion);
            MySqlConnection con = new MySqlConnection(cCon);
            con.Open();
            MySqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = sql;
            try
            {
                retorno = cmd1.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                string m = ex.Message;
                retorno = 0;
            }
            con.Close();
            return retorno;
        }
        public static void  GuardarArchivoServicio(TblArchivoServicios servicios)
        {
            //int retorno;
            //string sql = string.Format("insert into serv_detservicios (NroServicio, Cliente, Fecha, Tecnico, Descripcion, Observacion) Values('{0}','{1}','{2}')", );
            //SqlConnection con = conectarSql();
            //con.Open();
            //SqlCommand cmd1 = con.CreateCommand();
            //cmd1.CommandText = sql;
            //try
            //{
            //    retorno = cmd1.ExecuteNonQuery();
            //}
            //catch (MySqlException ex)
            //{
            //    string m = ex.Message;
            //    retorno = 0;
            //}
            //con.Close();
            //return retorno;
            using (SqlConnection Conn = conectarSql())
            {
                string comando = "insert into conds_servicio_archivo (nroservicio,nombre,archivo2,extension,mime,archivo) Values(@Solicitud,@Nombre,@File,@Extension,@Mime,@Archivo)";
                //comando = "insert into conds_servicio_archivo (nroservicio,nombre,extension,mime,archivo) Values(@Solicitud,@Nombre,@Extension,@Mime,@Archivo)";
                using (var sqlWrite = new SqlCommand(comando, Conn))
                {
                    sqlWrite.Parameters.Add("@Solicitud", SqlDbType.Int).Value = servicios.NroServicio;
                    sqlWrite.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = servicios.Nombre;
                    sqlWrite.Parameters.Add("@File", SqlDbType.VarBinary, int.MaxValue).Value = servicios.Archivo2;
                    sqlWrite.Parameters.Add("@Extension", SqlDbType.VarChar).Value = servicios.Extension;
                    sqlWrite.Parameters.Add("@Mime", SqlDbType.VarChar).Value = servicios.Mime;
                    sqlWrite.Parameters.Add("@Archivo", SqlDbType.VarChar).Value = servicios.Archivo;
                    sqlWrite.ExecuteNonQuery();
                }
            }
            
        }
        public static SqlConnection conectarSql()
        {

            string cCon = @"Data Source=" + Servidor + ";Initial Catalog=" + Base + ";Persist Security Info=True;Connection Timeout=300;User ID=" + Usuario + ";Password=" + PassBD;
            SqlConnection cnn = new SqlConnection(cCon);

            cnn.Open();
            return cnn;
        }

        public static SqlDataReader consultarSql(string consulta, SqlConnection con)
        {
            SqlCommand cmd2 = new SqlCommand(consulta, con);
            SqlDataReader res = cmd2.ExecuteReader();  // consulta.consultar(com           
            return res;
        }
    }
}