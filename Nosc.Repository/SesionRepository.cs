using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.Model;
using Nosc.DAL;

namespace Nosc.Repository
{
    public class SesionRepository
    {
        /// <summary>
        /// Obtiene la sesion. Si no se ha guardado sesion retorna null
        /// </summary>
        /// <returns></returns>
        public SesionModel GetSesion()
        {
            SesionModel sesion = null;

            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                try
                {
                    var s = (from o in entity.CAT_SESION.Include("APP_USUARIO")
                     select o).First<CAT_SESION>();

                    sesion = new SesionModel();
                    sesion.User = new UsuarioModel()
                    {
                        IdUsuario = s.IdUsuario,
                        NombreUsuario = s.APP_USUARIO.Nombre,
                        UsuarioCorreo = s.APP_USUARIO.UsuarioCorreo,
                        Puesto = s.APP_USUARIO.Puesto,
                        Apellido = s.APP_USUARIO.Apellido,
                        Area=s.APP_USUARIO.Area,
                        UsuarioPwd=s.APP_USUARIO.UsuarioCorreo,
                        IsActive = s.APP_USUARIO.IsActive
                    };
                    sesion.NoCerrarSesion = s.IsSaveSesion;

                }
                catch (Exception ex)
                {

                }
            }

            return sesion;
        }

        /// <summary>
        /// Guarda la ultima sesión exitosa
        /// </summary>
        /// <param name="sesion"></param>
        public void SetSesion(SesionModel sesion)
        {
            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                try
                {
                    var s = (from o in entity.CAT_SESION.Include("APP_USUARIO")
                             select o).FirstOrDefault();

                    if (s != null)
                    {
                        s.IdUsuario = sesion.User.IdUsuario;
                        s.IsSaveSesion = sesion.NoCerrarSesion;
                    }
                    else
                    {
                        entity.CAT_SESION.AddObject(new CAT_SESION()
                        {
                            IdSesion=1,
                            IdUsuario=sesion.User.IdUsuario,
                            IsSaveSesion = sesion.NoCerrarSesion
                        });
                    }

                    entity.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
