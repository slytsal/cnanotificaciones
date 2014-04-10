using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Nosc.Model;
using Nosc.DAL;

namespace Nosc.Repository
{
    public class NotificationRepository
    {

        public ObservableCollection<NotificacionModel> GetNotifications(UsuarioModel user, int runRefresh)
        {
            ObservableCollection<NotificacionModel> notifications = new ObservableCollection<NotificacionModel>();
            ObservableCollection<NotificacionModel> notificationsRecurrencia = new ObservableCollection<NotificacionModel>();
            //cuando arranca la aplicacion
            if (runRefresh ==0)
            {
                //se Recetean la tabla de NOTIFICACION_ACTIVA
                this.ResetNotificationActiva();
                notifications = this.LoadNotifications(user);
                //actualiza o inserta en la tabla NOTIFICACION_ACTIVA la bandera IsActiva en true
                if (notifications.Count != 0)
                    this.UpdateNotificationActiva(notifications);
                //Si la recurrencia es > 0 actualiza la fecha ultima muestra
                notifications.ToList().Where(r => r.Recurrencia > 0).ToList().ForEach(n =>
                {
                    n.FechaUltimaMuestra = new UNID().getNewUNID();
                    notificationsRecurrencia.Add(n);
                });

                //inserta los registros con recurrencia
                if (notificationsRecurrencia.Count != 0)
                    this.UpdateNotificationRecurrencia(notificationsRecurrencia);
                
            }
            else
            {
                notifications = this.LoadNotifications(user);
            }
            return notifications;
        }

        public ObservableCollection<NotificacionModel> LoadNotifications(UsuarioModel user)
        {
            
            ObservableCollection<NotificacionModel> notifications = new ObservableCollection<NotificacionModel>();

            long fechaActual = new UNID().getNewUNID();

            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                try
                {
                    //obtine las notificaciones con validaciones 
                    (from o in entity.Notificacions.Include("CAT_APP")
                     where
                          o.APP_USUARIO.IdUsuario == user.IdUsuario
                          &&
                          o.IsNotificacionActiva
                          &&
                          o.FechaNotificacion <= fechaActual
                     select new
                     {
                         o,
                         NotificacionActive = o.NOTIFICACION_ACTIVA.Select
                             (s =>
                             new Model.NotificacionActivaModel()
                             {
                                 IdNotificacion = s.IdNotificacion
                                 ,
                                 IdNotificacionActiva = s.IdNotificacionActiva
                                 ,
                                 IsActive = s.IsActive
                             }
                             )
                     }).ToList().ForEach(p =>
                     {
                         notifications.Add(new NotificacionModel()
                         {
                             App = new AppModel()
                             {
                                 AppName = p.o.CAT_APP.AppName,
                                 AppIconPath = p.o.CAT_APP.AppIcon,
                                 IdApp = p.o.CAT_APP.IdApp
                             },
                             FechaCreacion = p.o.FechaCreacion,
                             IdNotificacion = p.o.IdNotificacion,
                             Mensaje = p.o.Mensaje,
                             Parametro = p.o.Parametro,
                             Titulo = p.o.Titulo,
                             FechaNotificacion = p.o.FechaNotificacion,
                             Recurrencia = p.o.Recurrencia,
                             FechaUltimaMuestra = p.o.FechaUltimaMuestra,
                             NotificacionActiva = p.NotificacionActive,
                             IdUsuario = p.o.APP_USUARIO.IdUsuario,
                             Usuario = new UsuarioModel() 
                             {
                                 IdUsuario = p.o.APP_USUARIO.IdUsuario
                             }
                         });
                     });
                }
                catch (Exception ex)
                {

                }
            }
            return notifications;
        }

        /// <summary>
        /// Inserta una nueva notificacion en la tabla
        /// </summary>
        /// <param name="notif"></param>
        public void InsertNotification(NotificacionModel notif)
        {
            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                var res = (from o in entity.Notificacions
                           where o.IdNotificacion == notif.IdNotificacion
                           select o).FirstOrDefault();

                //insertar elemento ya que no existe
                if (res == null)
                {
                    entity.Notificacions.AddObject(new Notificacion()
                    {
                        Titulo=notif.Titulo,
                        Mensaje=notif.Mensaje,
                        Parametro=notif.Parametro,
                        IdApp=notif.App.IdApp,
                        IdUsuario=notif.Usuario.IdUsuario,
                        FechaCreacion=notif.FechaCreacion,
                        FechaNotificacion=notif.FechaNotificacion
                    });
                }

                entity.SaveChanges();

            }
        }

        /// <summary>
        /// Inserta el registro generando un nuevo unid
        /// </summary>
        /// <param name="notif"></param>
        /// <param name="GenerateId"></param>
        public void InsertNotification(NotificacionModel notif, bool GenerateId)
        {
            if (GenerateId)
            {
                notif.IdNotificacion = (new UNID()).getNewUNID();
            }

            this.InsertNotification(notif);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="notif"></param>
        public void UpdateNotification(NotificacionModel notif)
        {
            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                var res = (from o in entity.Notificacions
                           where o.IdNotificacion == notif.IdNotificacion
                           select o).FirstOrDefault();

                //actualiza el elemento ya que no existe
                if (res != null)
                {
                    res.Titulo = notif.Titulo;
                    res.Mensaje = notif.Mensaje;
                    res.Parametro = notif.Parametro;
                    res.IdApp = notif.App.IdApp;
                    res.IdUsuario = notif.Usuario.IdUsuario;
                    res.FechaCreacion = notif.FechaCreacion;
                    res.FechaNotificacion = notif.FechaNotificacion;
                    res.IsDescartarOnClick = notif.IsDescartarOnClick;
                    res.IsNotificacionActiva = notif.IsNotificacionActiva;
                }

                entity.SaveChanges();
            }


        }

        /// <summary>
        /// Actualiza o inserta cuando una notifcacion esta activa
        /// </summary>
        /// <param name="notif"></param>
        public void UpdateNotificationActiva( ObservableCollection<NotificacionModel> notifs)
        {
            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                if (notifs.Count !=0)
                {
                    notifs.ToList().ForEach(p =>
                    {
                        NOTIFICACION_ACTIVA result = null;
                        try
                        {
                            result = (from o in entity.NOTIFICACION_ACTIVA
                                       where o.IdNotificacion == p.IdNotificacion
                                       select o).FirstOrDefault();
                        }
                        catch (Exception)
                        {
                            
                        }
                        
                        //actualiza
                        if (result != null && !result.IsActive)
                        {
                            result.IsActive = true;
                        }
                        //inserta
                        if (result == null)
                        {
                            entity.NOTIFICACION_ACTIVA.AddObject(
                                  new NOTIFICACION_ACTIVA()
                            {
                                IdNotificacion = p.IdNotificacion
                                ,
                                IdNotificacionActiva = new UNID().getNewUNID()
                                ,
                                IsActive = true
                            });
                        }
                    });

                    entity.SaveChanges();
                    
                }
                
            }


        }

        /// <summary>
        /// Actualiza la fecha Fecha Ultima Muestra en la tabla NOTIFICACION_ACTIVA
        /// </summary>
        /// <param name="notif"></param>
        public void UpdateNotificationRecurrencia(ObservableCollection<NotificacionModel> notifs)
        {
            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                if (notifs.Count != 0)
                {
                    notifs.ToList().ForEach(p =>
                    {
                        Notificacion result = null;
                        try
                        {
                            result = (from o in entity.Notificacions
                                      where 
                                            o.IdNotificacion == p.IdNotificacion
                                            &&
                                            o.Recurrencia > 0
                                      select o).FirstOrDefault();
                        }
                        catch (Exception)
                        {

                        }

                        //actualiza
                        if (result != null)
                        {
                            result.FechaUltimaMuestra = p.FechaUltimaMuestra;
                        }
                    });

                    entity.SaveChanges();

                }

            }


        }

        /// <summary>
        /// Actualiza la fecha Fecha Ultima Muestra en la tabla NOTIFICACION_ACTIVA
        /// </summary>
        /// <param name="notif"></param>
        public void UpdateNotificationRecurrencia(NotificacionModel notif)
        {
            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                if (notif != null)
                {
                    Notificacion result = null;
                    try
                    {
                        result = (from o in entity.Notificacions
                                    where
                                        o.IdNotificacion == notif.IdNotificacion
                                        &&
                                        o.Recurrencia > 0
                                    select o).FirstOrDefault();
                    }
                    catch (Exception)
                    {

                    }

                    //actualiza
                    if (result != null)
                    {
                        result.FechaUltimaMuestra = notif.FechaUltimaMuestra;
                    }
                    entity.SaveChanges();

                }

            }


        }

        /// <summary>
        /// Actualiza todas las banderas en IsActive en false
        /// </summary>
        public void ResetNotificationActiva()
        {
            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                IEnumerable<NOTIFICACION_ACTIVA> result = new ObservableCollection<NOTIFICACION_ACTIVA>();
                
                try
                {
                    result = (from o in entity.NOTIFICACION_ACTIVA
                                select o).ToList();

                    (from o in result
                     select o).ToList().ForEach(p=> p.IsActive=false);

                    entity.SaveChanges();

                }
                catch (Exception)
                {

                }
            }
        }

        public ObservableCollection<NotificacionModel> LoadRecurrenceNotifications(UsuarioModel user)
        {

            ObservableCollection<NotificacionModel> notifications = new ObservableCollection<NotificacionModel>();
            ObservableCollection<NotificacionModel> notificationsRecurrencia = new ObservableCollection<NotificacionModel>();

            long fechaActual = new UNID().getNewUNID();

            using (var entity = new Nosc.DAL.NoscDBEntities())
            {
                try
                {
                    //obtine las notificaciones con validaciones 
                    (from o in entity.Notificacions.Include("CAT_APP")
                     where
                          o.APP_USUARIO.IdUsuario == user.IdUsuario
                          &&
                          o.IsNotificacionActiva
                          &&
                          o.FechaNotificacion <= fechaActual
                     select new
                     {
                         o,
                         NotificacionActive = o.NOTIFICACION_ACTIVA.Select
                             (s =>
                             new Model.NotificacionActivaModel()
                             {
                                 IdNotificacion = s.IdNotificacion
                                 ,
                                 IdNotificacionActiva = s.IdNotificacionActiva
                                 ,
                                 IsActive = s.IsActive
                             }
                             )
                     }).ToList().ForEach(p =>
                     {
                         notifications.Add(new NotificacionModel()
                         {
                             App = new AppModel()
                             {
                                 AppName = p.o.CAT_APP.AppName,
                                 AppIconPath = p.o.CAT_APP.AppIcon,
                                 IdApp = p.o.CAT_APP.IdApp
                             },
                             FechaCreacion = p.o.FechaCreacion,
                             IdNotificacion = p.o.IdNotificacion,
                             Mensaje = p.o.Mensaje,
                             Parametro = p.o.Parametro,
                             Titulo = p.o.Titulo,
                             FechaNotificacion = p.o.FechaNotificacion,
                             Recurrencia = p.o.Recurrencia,
                             FechaUltimaMuestra = p.o.FechaUltimaMuestra,
                             NotificacionActiva = p.NotificacionActive,
                         });
                     });

                    notifications.ToList().ForEach(p => 
                    {
                        if (p.Recurrencia > 0 )
                        {
                            notificationsRecurrencia.Add(p);    
                        }

                        p.NotificacionActiva.ToList().ForEach(a => 
                        {
                            if (!a.IsActive)
                            {
                                notificationsRecurrencia.Add(p);    
                            }
                        });

                    });


                }
                catch (Exception ex)
                {

                }
            }
            return notificationsRecurrencia;
        }

    }
}
