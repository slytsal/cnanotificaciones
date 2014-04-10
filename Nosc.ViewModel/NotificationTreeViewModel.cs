using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Nosc.Model;
using Nosc.Repository;

namespace Nosc.ViewModel
{
    public class NotificationTreeViewModel:ViewModelBase
    {
        public NotificationRepository NotificationRepository
        {
            get { return _NotificationRepository; }
            set
            {
                if (_NotificationRepository != value)
                {
                    _NotificationRepository = value;
                    OnPropertyChanged(NotificationRepositoryPropertyName);
                }
            }
        }
        private NotificationRepository _NotificationRepository;
        public const string NotificationRepositoryPropertyName = "NotificationRepository";

        public UsuarioModel Usuario
        {
            get { return _Usuario; }
            set
            {
                if (_Usuario != value)
                {
                    _Usuario = value;
                    OnPropertyChanged(UsuarioPropertyName);
                }
            }
        }
        private UsuarioModel _Usuario;
        public const string UsuarioPropertyName = "Usuario";

        public ObservableCollection<NotificationAppViewModel> Childrens
        {
            get { return _Childrens; }
            set
            {
                if (_Childrens != value)
                {
                    _Childrens = value;
                    OnPropertyChanged(TreeItemsPropertyName);
                }
            }
        }
        private ObservableCollection<NotificationAppViewModel> _Childrens;
        public const string TreeItemsPropertyName = "Childrens";

        public void GetTreeData(UsuarioModel user, int runRefresh)
        {
            ObservableCollection<NotificacionModel> notifications = this.NotificationRepository.GetNotifications(user,runRefresh);
            if (notifications.Count > 0)
            {
                (from o in notifications
                 select new AppModel() { IdApp=o.App.IdApp,AppName=o.App.AppName,AppIconPath=o.App.AppIconPath }
                    ).Distinct().ToList<AppModel>().ForEach(o =>
                 {
                     this.Childrens.Add(new NotificationAppViewModel()
                         {
                             App = o,
                             Notifications = this.GetNotification(notifications, o)
                         }
                     );
                 });
            }
        }

        private ObservableCollection<INotificationViewModel> GetNotification(ObservableCollection<NotificacionModel> notifications, AppModel app)
        {
            ObservableCollection<INotificationViewModel> nvm = new ObservableCollection<INotificationViewModel>();

            (from o in notifications
             where o.App.IdApp==app.IdApp
             select o).ToList<NotificacionModel>().ForEach(o =>
             {
                 nvm.Add(new NotificationViewModel()
                 {
                     Notification=o
                 });
             });
            
            return nvm;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationTreeViewModel(UsuarioModel user, int runRefresh)
        {
            this.NotificationRepository = new NotificationRepository();
            this.Childrens = new ObservableCollection<NotificationAppViewModel>();
            this.GetTreeData(user,runRefresh);
        }

        public void IDesactiveNotification(NotificacionModel notification)
        {
            if (notification !=null)
            {
                if (notification.Recurrencia==0  )
                {
                    notification.IsDescartarOnClick = true;
                    notification.IsNotificacionActiva = false;
                    this.NotificationRepository.UpdateNotification(notification);   
                }
            }
        }

    }
}
