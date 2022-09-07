using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AppSIMyS.Models;
using System.Threading.Tasks;

namespace AppSIMyS.Data
{
    public  class SQLiteHelper
    {
        SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Clientes>().Wait();
            db.CreateTableAsync<ClsServicios>().Wait();
            db.CreateTableAsync<ClsDetServicios>().Wait();
            db.CreateTableAsync<ClsEmpresas>().Wait();
            db.CreateTableAsync<ClsUsuarios>().Wait();
        }

        public Task <int> SaveCliente(Clientes cliente)
        {
            if (cliente.Rut != "")
            {                
                return db.InsertAsync(cliente);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// recuperar todos los clientes
        /// </summary>
        /// <returns></returns>
        public Task<List<Clientes>> GetClientesAsync()
        {
            return db.Table<Clientes>().ToListAsync();
        }
        /// <summary>
        /// Recuperar clientes por rut 
        /// </summary>
        /// <param name="Rut"> Rut del cliente</param>
        /// <returns></returns>
        
        public Task<Clientes> GetClienteByRutAsync(string Rut)
        {
            return db.Table<Clientes>().Where(a => a.Rut == Rut).FirstOrDefaultAsync();
        }

        public Task<List<ClsServicios>> GetServiciosAsync()
        {
            return db.Table<ClsServicios>().ToListAsync();
        }

        public Task<List<ClsEmpresas>> GetClsEmpresasAsync()
        {
            return db.Table<ClsEmpresas>().ToListAsync();
        }

        public Task<ClsEmpresas> GetClsEmpresasByRutAsync(string Rut)
        {
            return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).FirstOrDefaultAsync();
        }    
        public Task<List<ClsUsuarios>> GetUsuariosAsync()
        {
            return db.Table<ClsUsuarios>().ToListAsync();
        }

        public Task<ClsUsuarios> GetUsuariosByRutAsync(string Rut)
        {
            return db.Table<ClsUsuarios>().Where(a => a.rut == Rut).FirstOrDefaultAsync();
        }
    }
}
