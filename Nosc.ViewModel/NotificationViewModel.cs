using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.Model;

namespace Nosc.ViewModel
{
    public class NotificationViewModel:ViewModelBase,INotificationViewModel
    {
        public NotificacionModel Notification
        {
            get { return _Notification; }
            set
            {
                if (_Notification != value)
                {
                    _Notification = value;
                    OnPropertyChanged(NotificationPropertyName);
                }
            }
        }
        private NotificacionModel _Notification;
        public const string NotificationPropertyName = "Notification";

        public System.Collections.ObjectModel.ObservableCollection<INotificationViewModel> Childrens
        {
            get { return null;}
        }
    }
}
