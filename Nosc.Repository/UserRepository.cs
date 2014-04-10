using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Nosc.Model;
using Nosc.DAL;

namespace Nosc.Repository
{
    public class UserRepository
    {
        /// <summary>
        /// Retorna el usuario en base a un mail y un password. Retorna null si el usuario y pass no coinciden
        /// </summary>
        /// <param name="userMail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UsuarioModel GetUsuario(string userMail,string password)
        {
            UsuarioModel um = null;

            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                try
                {
                    var res=(from o in entity.APP_USUARIO
                     where o.UsuarioCorreo == userMail && o.UsuarioPwd == password
                     select o).First<APP_USUARIO>();

                    um = new UsuarioModel()
                    {
                        IdUsuario=res.IdUsuario,
                        UsuarioCorreo=res.UsuarioCorreo,
                        Apellido = res.Apellido,
                        Area = res.Area,
                        NombreUsuario = res.Nombre,
                        Puesto = res.Puesto,
                        UsuarioPwd = res.UsuarioPwd,
                        IsActive = res.IsActive
                    };
                }
                catch (Exception ex)
                {
                    ;
                }
            }

            return um;
        }

        /// <summary>
        /// En base al nombre del usuario obtener su informacion
        /// </summary>
        /// <param name="userMail"></param>
        /// <returns></returns>
        public UsuarioModel GetUsuario(string userMail)
        {
            UsuarioModel um = null;

            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                try
                {
                    var res = (from o in entity.APP_USUARIO
                               where o.UsuarioCorreo == userMail
                               select o).First<APP_USUARIO>();

                    um = new UsuarioModel()
                    {                        
                        IdUsuario = res.IdUsuario,
                        NombreUsuario = res.Nombre,
                        Area = res.Area,
                        Apellido =res.Apellido,
                        Puesto = res.Puesto,
                        UsuarioCorreo =res.UsuarioCorreo,
                        UsuarioPwd =res.UsuarioPwd,
                        IsActive = res.IsActive
                    };
                    
                }
                catch (Exception ex)
                {
                    ;
                }
            }

            return um;
        }
    }
}
