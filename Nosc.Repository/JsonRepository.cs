using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nosc.DAL;

namespace Nosc.Repository
{
    public class JsonRepository
    {
        public bool GetDeserializeServices(string json)
        {
            bool res = false;

            if (!String.IsNullOrEmpty(json))
            {
                try
                {
                    res = bool.Parse(json);
                }
                catch (Exception)
                {
                    
                    return false;
                }
            }

            return res;
        }

        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            try
            {
                Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                return resx;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List object
        /// </summary>
        /// <returns>Regresa List object </returns>
        /// <returns>Si no regresa null</returns>
        public string GetDeserialize(string listPocos, string nombreTabla)
        {
            string result = null;
            try
            {
                if (!string.IsNullOrEmpty(listPocos) && !string.IsNullOrEmpty(nombreTabla))
                {
                    switch (nombreTabla)
                    {
                        case "APP_USUARIO":
                            List<Model.SynClases.APP_USUARIO> listUsuario = new List<Model.SynClases.APP_USUARIO>();
                            listUsuario = JsonConvert.DeserializeObject<List<Model.SynClases.APP_USUARIO>>(listPocos);
                            if (listUsuario.Count() > 0)
                                this.LoadUsuario(listUsuario);
                            result = "Actualización e Inserción de: "+ nombreTabla +" correctamente.";
                            break;
                        case "CAT_APP":
                            List<Model.SynClases.CAT_APP> listApp = new List<Model.SynClases.CAT_APP>();
                            listApp = JsonConvert.DeserializeObject<List<Model.SynClases.CAT_APP>>(listPocos);
                            if (listApp.Count() > 0)
                                this.LoadApp(listApp);
                            result = "Actualización e Inserción de: " + nombreTabla + " correctamente.";
                            break;
                        case "Notificacion":
                            List<Model.SynClases.Notificacion> listNotificaciones = new List<Model.SynClases.Notificacion>();
                            listNotificaciones = JsonConvert.DeserializeObject<List<Model.SynClases.Notificacion>>(listPocos);
                            if (listNotificaciones.Count() > 0)
                                this.LoadNotificacion(listNotificaciones);
                            result = "Actualización e Inserción de: " + nombreTabla + " correctamente.";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public void LoadUsuario(List<Model.SynClases.APP_USUARIO> element)
        {
            try
            {
                if (element != null)
                {
                    using (NoscDBEntities entity = new NoscDBEntities())
                    {
                        element.ToList().ForEach(p =>
                        {
                            var query = (from cust in entity.APP_USUARIO
                                         where p.IdUsuario == cust.IdUsuario
                                         select cust).ToList();
                            //Actualización
                            if (query.Count > 0)
                            {
                                var aux = query.First();
                                if (aux.LastModifiedDate < p.LastModifiedDate)
                                    this.UpdateUsuario(p, entity);
                            }
                            //Inserción
                            else
                                this.InsertUsuario(p, entity);

                        });
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void UpdateUsuario(Model.SynClases.APP_USUARIO element, System.Data.Objects.ObjectContext context)
        {
            try
            {
                if (element != null && context != null)
                {
                    NoscDBEntities entity = context as NoscDBEntities;
                    if (entity != null)
                    {

                        Model.SynClases.APP_USUARIO update = element;
                        var modified = entity.APP_USUARIO.First(p => p.IdUsuario == update.IdUsuario);

                        modified.IdUsuario = update.IdUsuario;
                        modified.UsuarioCorreo = update.UsuarioCorreo;
                        modified.UsuarioPwd = update.UsuarioPwd;
                        modified.Nombre = update.Nombre;
                        modified.Apellido = update.Apellido;
                        modified.Area = update.Area;
                        modified.Puesto = update.Puesto;
                        modified.IsActive = update.IsActive;
                        modified.IsModified = update.IsModified;
                        modified.LastModifiedDate = update.LastModifiedDate;
                        modified.ServerLastModifiedDate = update.ServerLastModifiedDate;
                        modified.IsNewUser = update.IsNewUser;
                        modified.IsVerified = update.IsVerified;
                        modified.IsMailSent = update.IsMailSent;
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void InsertUsuario(Model.SynClases.APP_USUARIO element, System.Data.Objects.ObjectContext context)
        {
            try
            {
                if (element != null && context != null)
                {
                    NoscDBEntities entity = context as NoscDBEntities;
                    if (entity != null)
                    {
                        APP_USUARIO insert = new APP_USUARIO()
                        {
                            IdUsuario = element.IdUsuario,
                            UsuarioCorreo = element.UsuarioCorreo,
                            UsuarioPwd = element.UsuarioPwd,
                            Nombre = element.Nombre,
                            Apellido = element.Apellido,
                            Area = element.Area,
                            Puesto = element.Puesto,
                            IsActive = element.IsActive,
                            IsModified = element.IsModified,
                            LastModifiedDate = element.LastModifiedDate,
                            ServerLastModifiedDate = element.ServerLastModifiedDate,
                            IsNewUser = element.IsNewUser,
                            IsVerified = element.IsVerified,
                            IsMailSent = element.IsMailSent
                        };
                        entity.APP_USUARIO.AddObject(insert);
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void LoadNotificacion(List<Model.SynClases.Notificacion> element)
        {
            try
            {
                if (element != null)
                {
                    using (NoscDBEntities entity = new NoscDBEntities())
                    {
                        element.ToList().ForEach(p =>
                        {
                            var query = (from cust in entity.Notificacions
                                         where p.IdNotificacion == cust.IdNotificacion
                                         select cust).ToList();
                            //Actualización
                            if (query.Count > 0)
                            {
                                var aux = query.First();
                                if (aux.LastModifiedDate < p.LastModifiedDate)
                                    this.UpdateNotificacion(p, entity);
                            }
                            //Inserción
                            else
                                this.InsertNotificacion(p, entity);

                        });
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void UpdateNotificacion(Model.SynClases.Notificacion element, System.Data.Objects.ObjectContext context)
        {
            try
            {
                if (element != null && context != null)
                {
                    NoscDBEntities entity = context as NoscDBEntities;
                    if (entity != null)
                    {

                        Model.SynClases.Notificacion update = element;
                        var modified = entity.Notificacions.First(p => p.IdUsuario == update.IdUsuario);

                        modified.IdNotificacion = update.IdNotificacion;
                        modified.FechaCreacion = update.FechaCreacion;
                        modified.Parametro = update.Parametro;
                        modified.Mensaje = update.Mensaje;
                        modified.Titulo = update.Titulo;
                        modified.IdUsuario = update.IdUsuario;
                        modified.IdApp = update.IdApp;
                        modified.FechaNotificacion = update.FechaNotificacion;
                        modified.IsCerrado = update.IsCerrado;
                        modified.FechaCerrado = update.FechaCerrado;
                        modified.CanCerrar = update.CanCerrar;
                        modified.IsModified = update.IsModified;
                        modified.LastModifiedDate = update.LastModifiedDate;
                        modified.ServerLastModifiedDate = update.ServerLastModifiedDate;
                        modified.Recurrencia = update.Recurrencia;
                        modified.FechaUltimaMuestra = update.FechaUltimaMuestra;
                        modified.IsNotificacionActiva = update.IsNotificacionActiva;
                        modified.IsDescartarOnClick = update.IsDescartarOnClick;

                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void InsertNotificacion(Model.SynClases.Notificacion element, System.Data.Objects.ObjectContext context)
        {
            try
            {
                if (element != null && context != null)
                {
                    NoscDBEntities entity = context as NoscDBEntities;
                    if (entity != null)
                    {
                        Notificacion insert = new Notificacion()
                        {
                            IdNotificacion = element.IdNotificacion,
                            FechaCreacion = element.FechaCreacion,
                            Parametro = element.Parametro,
                            Mensaje = element.Mensaje,
                            Titulo = element.Titulo,
                            IdUsuario = element.IdUsuario,
                            IdApp = element.IdApp,
                            FechaNotificacion = element.FechaNotificacion,
                            IsCerrado = element.IsCerrado,
                            FechaCerrado = element.FechaCerrado,
                            CanCerrar = element.CanCerrar,
                            IsModified = element.IsModified,
                            LastModifiedDate = element.LastModifiedDate,
                            ServerLastModifiedDate = element.ServerLastModifiedDate,
                            Recurrencia = element.Recurrencia,
                            FechaUltimaMuestra = element.FechaUltimaMuestra,
                            IsNotificacionActiva = element.IsNotificacionActiva,
                            IsDescartarOnClick = element.IsDescartarOnClick
                        };
                        entity.Notificacions.AddObject(insert);
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void LoadApp(List<Model.SynClases.CAT_APP> element)
        {
            try
            {
                if (element != null)
                {
                    using (NoscDBEntities entity = new NoscDBEntities())
                    {
                        element.ToList().ForEach(p =>
                        {
                            var query = (from cust in entity.CAT_APP
                                         where p.IdApp == cust.IdApp
                                         select cust).ToList();
                            //Actualización
                            if (query.Count > 0)
                            {
                                var aux = query.First();
                                if (aux.LastModifiedDate < p.LastModifiedDate)
                                    this.UpdateApp(p, entity);
                            }
                            //Inserción
                            else
                                this.InsertApp(p, entity);

                        });
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void UpdateApp(Model.SynClases.CAT_APP element, System.Data.Objects.ObjectContext context)
        {
            try
            {
                if (element != null && context != null)
                {
                    NoscDBEntities entity = context as NoscDBEntities;
                    if (entity != null)
                    {

                        Model.SynClases.CAT_APP update = element;
                        var modified = entity.CAT_APP.First(p => p.IdApp == update.IdApp);

                        modified.IdApp = update.IdApp;
                        modified.AppName = update.AppName;
                        modified.AppIcon = update.AppIcon;
                        modified.IsModified = update.IsModified;
                        modified.LastModifiedDate = update.LastModifiedDate;
                        modified.ServerLastModifiedDate = update.ServerLastModifiedDate;
                        modified.IsActive = update.IsActive;

                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void InsertApp(Model.SynClases.CAT_APP element, System.Data.Objects.ObjectContext context)
        {
            try
            {
                if (element != null && context != null)
                {
                    NoscDBEntities entity = context as NoscDBEntities;
                    if (entity != null)
                    {
                        CAT_APP insert = new CAT_APP()
                        {
                            IdApp = element.IdApp,
                            AppName = element.AppName,
                            AppIcon = element.AppIcon,
                            IsModified = element.IsModified,
                            LastModifiedDate = element.LastModifiedDate,
                            ServerLastModifiedDate = element.ServerLastModifiedDate,
                            IsActive = element.IsActive
                        };

                        entity.CAT_APP.AddObject(insert);
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
