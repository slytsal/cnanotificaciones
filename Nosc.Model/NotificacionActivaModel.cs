using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Model
{
    public class NotificacionActivaModel : ModelBase
    {
        public long IdNotificacionActiva
        {
            get { return _IdNotificacionActiva; }
            set
            {
                if (_IdNotificacionActiva != value)
                {
                    _IdNotificacionActiva = value;
                    OnPropertyChanged(IdNotificacionActivaPropertyName);
                }
            }
        }
        private long _IdNotificacionActiva;
        public const string IdNotificacionActivaPropertyName = "IdNotificacionActiva";

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";

        public long IdNotificacion
        {
            get { return _IdNotificacion; }
            set
            {
                if (_IdNotificacion != value)
                {
                    _IdNotificacion = value;
                    OnPropertyChanged(IdNotificacionPropertyName);
                }
            }
        }
        private long _IdNotificacion;
        public const string IdNotificacionPropertyName = "IdNotificacion";

        public NotificacionModel Notificacion
        {
            get { return _Notificacion; }
            set
            {
                if (_Notificacion != value)
                {
                    _Notificacion = value;
                    OnPropertyChanged(NotificacionPropertyName);
                }
            }
        }
        private NotificacionModel _Notificacion;
        public const string NotificacionPropertyName = "Notificacion";


    }
}
