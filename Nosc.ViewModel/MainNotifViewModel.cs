using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.Model;
using System.Windows.Threading;

namespace Nosc.ViewModel
{
    public class MainNotifViewModel : ViewModelBase
    {
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

        public NotificationTreeViewModel NotificationTreeView
        {
            get { return _NotificationTreeView; }
            set
            {
                if (_NotificationTreeView != value)
                {
                    _NotificationTreeView = value;
                    OnPropertyChanged(NotificationTreeViewPropertyName);
                }
            }
        }
        private NotificationTreeViewModel _NotificationTreeView;
        public const string NotificationTreeViewPropertyName = "NotificationTreeView";

        public ConexionServerViewModel ConexionServer
        {
            get { return _ConexionServer; }
            set
            {
                if (_ConexionServer != value)
                {
                    _ConexionServer = value;
                    OnPropertyChanged(ConexionServerPropertyName);
                }
            }
        }
        private ConexionServerViewModel _ConexionServer;
        public const string ConexionServerPropertyName = "ConexionServer";

        public SyncViewModel SyncViewModel
        {
            get { return _SyncViewModel; }
            set
            {
                if (_SyncViewModel != value)
                {
                    _SyncViewModel = value;
                    OnPropertyChanged(SyncViewModelPropertyName);
                }
            }
        }
        private SyncViewModel _SyncViewModel;
        public const string SyncViewModelPropertyName = "SyncViewModel";

        public MainNotifViewModel(UserLoginViewModel userLogin, Dispatcher dispatcher) 
        {
            this.Usuario = userLogin.User;
            
            //this.ConexionServer = new ConexionServerViewModel();
            this.SyncViewModel = new SyncViewModel();

            //indica cuando arrancla la aplicacion por primera vez =0
            this.LoadInfo(0);
        }

        public void LoadInfo(int runRefresh) 
        {
            this.NotificationTreeView = new NotificationTreeViewModel(this.Usuario,runRefresh);
        }
        
    }
}
