using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AppSIMyS.Models;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

namespace AppSIMyS.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            // db.DropTableAsync<ClsEmpresas>().Wait();
            //db.DropTableAsync<TblImagenServicio>().Wait();
            db.CreateTableAsync<ClsClientes>().Wait();
            db.CreateTableAsync<ClsServicios>().Wait();
            db.CreateTableAsync<ClsDetServicios>().Wait();
            db.CreateTableAsync<ClsEmpresas>().Wait();
            db.CreateTableAsync<ClsUsuarios>().Wait();
            db.CreateTableAsync<TblImagenServicio>().Wait();
            db.CreateTableAsync<TblUsuarios>().Wait();
            db.CreateTableAsync<TblServicios>().Wait();
            db.CreateTableAsync<ClsDetServicios>().Wait();
        }

        public Task<int> SaveCliente(ClsClientes cliente)
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
        public Task<List<ClsClientes>> GetClientesAsync()
        {
            return db.Table<ClsClientes>().ToListAsync();
        }

        public IEnumerable<ClsClientes> GetClientesByRutAsync2(string Rut)
        {
            var result = db.QueryAsync<ClsClientes>("select * from ClsClientes where rut = ?", Rut);
            return result.Result;
        }

        /// <summary>
        /// Recuperar clientes por rut 
        /// </summary>
        /// <param name="Rut"> Rut del cliente</param>
        /// <returns></returns>

        public Task<ClsClientes> GetClienteByRutAsync(string Rut)
        {
            return db.Table<ClsClientes>().Where(a => a.Rut == Rut).FirstOrDefaultAsync();
        }


        public Task<List<TblServicios>> GetServiciosAsync()
        {
            return db.Table<TblServicios>().ToListAsync();
        }

        public Task<List<ClsEmpresas>> GetClsEmpresasAsync()
        {
            return db.Table<ClsEmpresas>().ToListAsync();
        }

        public Task<ClsEmpresas> GetClsEmpresasByRutAsync(string Rut)
        {
            return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).FirstOrDefaultAsync();
        }
        public IEnumerable<ClsEmpresas> GetClsEmpresasByRutAsync2(string Rut)
        {
            var result = db.QueryAsync<ClsEmpresas>("select * from ClsEmpresas where rut = ?", Rut);
            return result.Result;
        }
        public IEnumerable<ClsEmpresas> GetClsEmpresasByAsync2()
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result = db.QueryAsync<ClsEmpresas>("select * from ClsEmpresas");
            return result.Result;
        }
        public void EliminarClsEmpresas()
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result = db.QueryAsync<ClsEmpresas>("delete from ClsEmpresas");
            //return result.Result;
        }
        public void EliminarClsClientes()
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result = db.QueryAsync<ClsEmpresas>("delete from ClsClientes");
            //return result.Result;
        }
        public void AgregarClsEmpresas(ClsEmpresas empresas)
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result = db.QueryAsync<ClsEmpresas>("insert into ClsEmpresas (empresa, descripcion, rut, direccion, telefono, email, logo, piepagina) values (?,?,?,?,?,?,?,?)", empresas.Empresa, empresas.Descripcion, empresas.Rut, empresas.Direccion, empresas.Telefono, empresas.Email, empresas.Logo, empresas.PiePagina);
            //return result.Result;
        }
        public void AgregarClsClientes(ClsClientes cliente)
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result = db.QueryAsync<ClsEmpresas>("insert into ClsClientes (rut, empresa, descripcion, telefono, url,  direccion, logo, email) values (?,?,?,?,?,?,?,?)", cliente.Rut, cliente.Empresa, cliente.Descripcion, cliente.Telefono, cliente.Url, cliente.Direccion, cliente.Logo, cliente.Email);
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


        // imagenes
        public void EliminarTblImagenServicio()
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result = db.QueryAsync<TblImagenServicio>("delete from TblImagenServicio");
            //return result.Result;
        }

        public IEnumerable<TblImagenServicio> GetTblImagenesByAsync2()
        {
            //return db.Table<ClsEmpresas>().Where(a => a.Empresa == Rut).ToListAsync().ConfigureAwait(false);
            var result = db.QueryAsync<TblImagenServicio>("select * from TblImagenServicio order by id ");
            return result.Result;
        }

        public void AgregarTblImagenServicio(TblImagenServicio imagen)
        {
            var result = db.QueryAsync<ClsEmpresas>("insert into TblImagenServicio (empresa, comentario, imagen) values (?,?,?)", imagen.Empresa, imagen.Comentario, imagen.Imagen);
        }
        public void EliminarTblImagenServicio(int  Id)
        {
            var result = db.QueryAsync<ClsEmpresas>("delete from TblImagenServicio where id=?", Id);
        }
        public void ActualizarTblImagenServicio(int Id, string Comentario)
        {
            var result = db.QueryAsync<ClsEmpresas>("update TblImagenServicio set comentario=? where id=?", Comentario, Id);
        }
        // fin imagenes

        // usuarios
        public void EliminarTblUsuarios()
        {
            var result = db.QueryAsync<TblUsuarios>("delete from TblUsuarios");
        }
        public void AgregarTblUsuarios(TblUsuarios cliente)
        {
            var result = db.QueryAsync<TblUsuarios>("insert into TblUsuarios (rut, nombres, email, password) values (?,?,?,?)", cliente.rut, cliente.nombres, cliente.email, cliente.password);
        }

        public IEnumerable<TblUsuarios> GetUsuario(string usuario, string password )
        {
            var result = db.QueryAsync<TblUsuarios>("select * from TblUsuarios where email = ? and password = ? ", usuario, password);
            return result.Result;
        }

        public IEnumerable<TblUsuarios> GetUsuariosByRutAsync2(string Rut)
        {
            var result = db.QueryAsync<TblUsuarios>("select * from TblUsuarios where rut = ?", Rut);
            return result.Result;
        }
        // fin usuarios
        
        // Servicios
        public void EliminarTblServicios()
        {
            var result = db.QueryAsync<TblServicios>("delete from TblServicios");
        }
        public void AgregarTblServicios(TblServicios cliente)
        {
            var result = db.QueryAsync<TblServicios>("insert into TblServicios (codigo, descripcion) values (?,?)", cliente.Codigo, cliente.Descripcion);
        }

        // fin servicios
        // Detalle Servicios
        public void EliminarClsDetServicios()
        {
            var result = db.QueryAsync<ClsDetServicios>("delete from ClsDetServicios");
        }
        public void AgregarClsDetServicios(ClsDetServicios cliente)
        {
            var result = db.QueryAsync<ClsDetServicios>("insert into ClsDetServicios (codigo, cantidad, comentario, descripcion) values (?,?,?,?)", cliente.Codigo, cliente.Cantidad, cliente.Comentario, cliente.Descripcion);
        }

        public IEnumerable<ClsDetServicios> GetClsDetServiciosByRutAsync2()
        {
            var result = db.QueryAsync<ClsDetServicios>("select * from ClsDetServicios order by id ");
            return result.Result;
        }
        public void EliminarClsDetServicios(int Id)
        {
            var result = db.QueryAsync<ClsDetServicios>("delete from ClsDetServicios where id=?", Id);
        }
        public void ActualizarClsDetServicios(int Id, string Comentario, int cantidad)
        {
            var result = db.QueryAsync<ClsDetServicios>("update ClsDetServicios set comentario=?,cantidad=? where id=?", Comentario, cantidad, Id);
        }

        public IEnumerable<ClsDetServicios> GetDetServiciosByRutAsync2()
        {
            var result = db.QueryAsync<ClsDetServicios>("select * from ClsDetServicios ");
            return result.Result;
        }

        // fin Detalle  servicios
    }
}



