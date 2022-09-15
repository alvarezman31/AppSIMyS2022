using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AppSIMyS.Models;
using System.Threading.Tasks;
using System.Collections;

namespace AppSIMyS.Data
{
    public class SQLiteHelper
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

        public Task<int> SaveCliente(Clientes cliente)
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
        public Task<int> SaveEmpresa(ClsEmpresas empresa)
        {
            if (empresa.Rut != "")
            {
                return db.InsertAsync(empresa);
            }
            else
            {
                return null;
            }
        }
        public Task<int> UpdateEmpresa(ClsEmpresas empresa)
        {
            if (empresa.Rut != "")
            {
                return db.UpdateAsync(empresa);
            }
            else
            {
                return null;
            }
        }
        public Task DeleteEmpresas()
        {
            db.DeleteAllAsync<ClsEmpresas>();
            return null;

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
        public  IEnumerable<ClsEmpresas> GetClsEmpresasByRutAsync2(string Rut)
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result =  db.QueryAsync<ClsEmpresas>("select top 1 * from ClsEmpresas where rut = ?", Rut);
            return result.Result;
        }
        public  IEnumerable<ClsEmpresas> GetClsEmpresasByAsync2()
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result =  db.QueryAsync<ClsEmpresas>("select * from ClsEmpresas");
            return result.Result;
        }
        public void  EliminarClsEmpresas()
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result =  db.QueryAsync<ClsEmpresas>("delete from ClsEmpresas");
            //return result.Result;
        }
        public void  AgregarClsEmpresas(ClsEmpresas empresas)
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result =  db.QueryAsync<ClsEmpresas>("insert into ClsEmpresas (empresa, descripcion, rut, direccion, telefono, email, logo) values (?,?,?,?,?,?,?)",empresas.Empresa,empresas.Descripcion,empresas.Rut, empresas.Direccion, empresas.Telefono, empresas.Empresa, empresas.Logo);
            //return result.Result;
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



