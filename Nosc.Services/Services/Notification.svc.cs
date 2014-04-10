using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Nosc.Services.Repository.Repository;
using Nosc.Model;

namespace Nosc.Services.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Notification : INotification
    {
        public long GetRegisterNotification(string Notification)
        {

            #region propiedades
            long returnIdNotificacion = 0;
            NotificationRepository _NotificationRepository = new  NotificationRepository();
            NotificacionModel notificacion = null;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(Notification))
                {
                    //Deserializa 
                    notificacion = _NotificationRepository.GetDeserializeNotificacion(Notification);
                    //Inserta a la tabla Notificacion y trae la evidencia 
                    returnIdNotificacion = _NotificationRepository.InsertNotification(notificacion);
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return returnIdNotificacion;
            #endregion
            
        }

        public string GetModifyNotification(string Notification)
        {
            #region propiedades
            NotificationRepository _NotificationRepository = new NotificationRepository();
            NotificacionModel notificacion = null;
            string response = null;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(Notification))
                {
                   //Deserializa 
                   notificacion = _NotificationRepository.GetDeserializeNotificacion(Notification);
                   //actualizala bandera  IsNotificacionActiva en la  tabla Notificacion
                   response=  _NotificationRepository.UpdateNotification(notificacion);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return response;
            #endregion
        }
    }
}
