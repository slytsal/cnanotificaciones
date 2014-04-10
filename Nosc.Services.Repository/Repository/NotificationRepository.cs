using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.Model;
using Nosc.Services.Dal.POCOS;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Data.Objects;

namespace Nosc.Services.Repository.Repository
{
    public class NotificationRepository
    {
        /// <summary>
        /// Inserta una nueva notificacion en la tabla
        /// </summary>
        /// <param name="notif"></param>
        public long InsertNotification(NotificacionModel notif)
        {
            long idNotificacion = 0;
            using (var entity = new NoscDBEntities())
            {
                var res = (from o in entity.Notificacions
                           where o.IdNotificacion == notif.IdNotificacion
                           select o).FirstOrDefault();

                //insertar elemento ya que no existe
                if (res == null)
                {
                    idNotificacion = new UNID().getNewUNID();
                    try
                    {
                        entity.Notificacions.AddObject(new Notificacion()
                        {
                            IdNotificacion = idNotificacion,
                            Titulo = notif.Titulo,
                            Mensaje = notif.Mensaje,
                            Parametro = notif.Parametro,
                            IdApp = notif.IdApp,
                            IdUsuario = notif.IdUsuario,
                            FechaCreacion = new UNID().getNewUNID(),
                            FechaNotificacion = notif.FechaNotificacion,
                            IsCerrado = notif.IsCerrado,
                            CanCerrar = notif.CanCerrar,
                            IsModified = false,
                            LastModifiedDate = new UNID().getNewUNID(),
                            Recurrencia = 30,
                            FechaUltimaMuestra = new UNID().getNewUNID(),
                            IsNotificacionActiva = true,
                            IsDescartarOnClick = false
                        });

                        entity.SaveChanges();
                        UpdateSync("Notificacion", entity);
                    }
                    catch (Exception)
                    {

                        return 0;
                    }
                    
                }

            }
            return idNotificacion;
        }

        /// <summary>
        /// Desactiva Notificaciones (IsNotificacionActiva)
        /// </summary>
        /// <param name="notif"></param>
        public string UpdateNotification(NotificacionModel notif)
        {
            string response =null;
            using (var entity = new NoscDBEntities())
            {
                try
                {
                    var res = (from o in entity.Notificacions
                               where o.IdNotificacion == notif.IdNotificacion
                               select o).FirstOrDefault();

                    //actualiza el elemento dependiendo el parametro
                    if (res != null)
                    {
                        res.IsNotificacionActiva = notif.IsNotificacionActiva;
                        res.LastModifiedDate = new UNID().getNewUNID();

                        entity.SaveChanges();
                        response = "Exito";
                        UpdateSync("Notificacion", entity);
                    }

                }
                catch (Exception ex)
                {
                    
                    return ex.Message;
                }
            }

            return response;
        }

        public NotificacionModel GetDeserializeNotificacion(string notif)
        {
            NotificacionModel res = null;

            if (!String.IsNullOrEmpty(notif))
                res = JsonConvert.DeserializeObject<NotificacionModel>(notif);

            return res;
        }

        public void UpdateSync(string nameTable, ObjectContext context)
        {
            if (!String.IsNullOrEmpty(nameTable) && context != null)
            {
                NoscDBEntities entity = context as NoscDBEntities;
                if (entity != null)
                {
                    MODIFIEDDATA result = null;
                    try
                    {
                        result = (from o in entity.SYNCTABLE
                                  join r in entity.MODIFIEDDATA
                                  on o.IdSyncTable equals r.IdSyncTable
                                  where o.SyncTableName == nameTable
                                  select r).First();
                    }
                    catch (Exception)
                    {
                        ;
                    }
                    if (result != null)
                    {
                        result.IsModified = true;
                        entity.SaveChanges();
                    }
                }
            }
        }

    }
}
