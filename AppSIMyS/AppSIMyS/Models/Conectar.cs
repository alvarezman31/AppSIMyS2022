using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppSIMyS.Models
{
    public class Conectar
    {
        //public static string cCon = "Server=srv29.cpanelhost.cl;UserID=cal42270_wln;Database=cal42270_worldlan;Password=Wln.2020;";
        public static string cCon = "Server=alvaluc.com;UserID=amdappserv;Database=alvalucc_appserv;Password=M@@L+2209;";

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

    }
}