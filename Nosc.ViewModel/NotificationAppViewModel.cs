using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Nosc.Model;

namespace Nosc.ViewModel
{
    public class NotificationAppViewModel:ViewModelBase,INotificationViewModel
    {
        public ObservableCollection<INotificationViewModel> Notifications
        {
            get { return _Notifications; }
            set
            {
                if (_Notifications != value)
                {
                    _Notifications = value;
                    OnPropertyChanged(NotificationsPropertyName);
                }
            }
        }
        private ObservableCollection<INotificationViewModel> _Notifications;
        public const string NotificationsPropertyName = "Notifications";

        public AppModel App
        {
            get { return _App; }
            set
            {
                if (_App != value)
                {
                    _App = value;
                    OnPropertyChanged(AppPropertyName);
                }
            }
        }
        private AppModel _App;
        public const string AppPropertyName = "App";

        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set
            {
                if (_IsExpanded != value)
                {
                    _IsExpanded = value;
                    OnPropertyChanged(IsExpandedPropertyName);
                }
            }
        }
        private bool _IsExpanded;
        public const string IsExpandedPropertyName = "IsExpanded";

        public void GetNotifications()
        {

        }

        public ObservableCollection<INotificationViewModel> Childrens
        {
            get
            {
                return this.Notifications;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationAppViewModel()
        {
            this.IsExpanded = true;
        }
    }
}
