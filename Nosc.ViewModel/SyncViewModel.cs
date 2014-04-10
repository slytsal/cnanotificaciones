using System;
using System.Collections.Generic;
using Nosc.Repository.Sync;
using System.Configuration;
using RestSharp;
using System.Windows.Threading;
using System.Net.NetworkInformation;
using System.Collections.ObjectModel;
using Nosc.Model;
using Nosc.Repository;
using Actualizador.WebService.ViewModel;
using System.Threading;

namespace Nosc.ViewModel
{
    public class SyncViewModel:ViewModelBase
    {
        public Dispatcher _Dispatcher;
        public MainNotifViewModel _MainNotifViewModel;
        private string VersionAssembly = "";
        private string IdUsuario = "";
        private bool IsActualizando = false;
        public ObservableCollection< NotificationAppViewModel> _NotificationApp;
        public IRecurrence _Recurrence;
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

        #region propiedades de la conexion a los servicios

        JsonRepository json = new JsonRepository();

        public bool ConexionServer
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
        private bool _ConexionServer;
        public const string ConexionServerPropertyName = "ConexionServer";

        public string UrlConexion
        {
            get { return _UrlConexion; }
            set
            {
                if (_UrlConexion != value)
                {
                    _UrlConexion = value;
                    OnPropertyChanged(UrlConexionPropertyName);
                }
            }
        }
        private string _UrlConexion;
        public const string UrlConexionPropertyName = "UrlConexion";

        public string BackgroundConexion
        {
            get { return _BackgroundConexion; }
            set
            {
                if (_BackgroundConexion != value)
                {
                    _BackgroundConexion = value;
                    OnPropertyChanged(BackgroundConexionPropertyName);
                }
            }
        }
        private string _BackgroundConexion;
        public const string BackgroundConexionPropertyName = "BackgroundConexion";

        public string MessageConexion
        {
            get { return _MessageConexion; }
            set
            {
                if (value != _MessageConexion)
                {
                    this._MessageConexion = value;
                    OnPropertyChanged("MessageConexion");
                }
            }
        }
        string _MessageConexion;
        #endregion

        #region Instancias.
        static BroadCastRepository _BroadCast = new BroadCastRepository();
        static ReciverRepository _Reciver = new ReciverRepository();
        #endregion

        #region Propiedades y Metodos Hilo
        public static bool IsRunning = false;        
        System.Timers.Timer t;

        public string Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    this._message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
        string _message;

        public bool JobDone
        {
            get { return _jobDone; }
            set
            {
                if (value != _jobDone)
                {
                    this._jobDone = value;
                    OnPropertyChanged("JobDone");
                }
            }
        }
        bool _jobDone;

        public void start()
        {
            this.JobDone = false;
            this.OnPropertyChanged("JobDone");
            SyncViewModel.IsRunning = true;
            //t.Start();
            Thread sync = new Thread(() => this.SyncService());
            sync.IsBackground = true;
            sync.Start();
        }
        #endregion

        #region Propiedades Servicios web
        string routeServiceValideConexion;
        string routeServiceDownLoad;
        string routeServiceUpLoad;
        string basicAuthUser;
        string basicAuthPass;

        #endregion

        #region Constructors
        public SyncViewModel(Dispatcher dispatcher, MainNotifViewModel mainNotifViewModel, IRecurrence recurrence,string Assembly)
        {
            this.LoadPropiedades();
            this._Dispatcher = dispatcher;
            this._MainNotifViewModel = mainNotifViewModel;
            this._Recurrence = recurrence;
            this.Message = "Sincronizando...";
            this.VersionAssembly = Assembly;
            this._jobDone = false;
            this.IdUsuario = mainNotifViewModel.Usuario.IdUsuario.ToString();
        }

        public SyncViewModel()
        {
        }
        #endregion

        #region Metodo carga propiedades.
        private void LoadPropiedades()
        {
            this.Message = "Cargando Propiedades.";

            routeServiceValideConexion = ConfigurationManager.AppSettings["ValideConexion"].ToString();
            routeServiceDownLoad = ConfigurationManager.AppSettings["BroadCast"].ToString();
            routeServiceUpLoad = ConfigurationManager.AppSettings["Reciver"].ToString();
            basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
            basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();

            this.NotificationRepository = new NotificationRepository();
        }
        #endregion

        #region BroadCast (Descarga)
        public bool CallModifiedData()
        {
            this.Message = "Descargando Información...";
            bool responseSevice = false;
            string nameService = "GetModifiedData";

            LoadPropiedades();
            var client = new RestClient(routeServiceDownLoad);
            client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
            var request = new RestRequest(Method.POST);
            request.Resource = nameService;
            this.Message = "Obteniendo el metodo " + nameService;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            try
            {
                this.Message = "Obteniendo respuesta..";
                IRestResponse response = client.Execute(request);
                var content = response.Content;                
                this.Message = content.ToString();                
                int inicio = content.IndexOf(":", 0) + 2;
                int fin = content.IndexOf("]", inicio) + 1;
                content = content.Substring(inicio, content.Length - (inicio + 2));
                List<string> lstTablas = null;
                lstTablas = _BroadCast.GetTableNameModifiedData(content);
                if (lstTablas != null & lstTablas.Count > 0)
                {
                    foreach (var item in lstTablas)
                    {
                        try
                        {
                            string jsonMax = _BroadCast.GetMaxTableLocal(item);
                            if (!String.IsNullOrEmpty(jsonMax))
                            {
                                string jsonTable = this.CallGetTablesUpdate(item, jsonMax);
                                if (!string.IsNullOrEmpty(jsonTable))
                                {
                                    _BroadCast.SP_UpdateModifiedDataLocal(item, content);
                                    responseSevice = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return responseSevice;
        }

        public string CallGetTablesUpdate(string _tableName, string _maxJson)
        {
            string MSJ = "";
            string nameService = "GetTablesUpdate";            
            var client = new RestClient(routeServiceDownLoad);
            client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
            var request = new RestRequest(Method.POST);
            request.Resource = nameService;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            request.AddBody(new { tableName = _tableName, maxJson = _maxJson });
            try
            {
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                JsonRepository _JsonRepository = new JsonRepository();
                Dictionary<string, string> resx = _JsonRepository.GetResponseDictionary(response.Content);

                //inserta o actualiza los registros correspondientes
                MSJ = _JsonRepository.GetDeserialize(resx["GetTablesUpdateResult"], _tableName);

            }
            catch (Exception)
            {

                throw;
            }
            return MSJ;
        }
        #endregion

        #region Reciver (Subida)
        public bool CallService()
        {
            bool responseSevice = false;

            string nameService = "LoadCatGeneric";

            List<string> lstIsModified = _Reciver.GetIsModified();
            if (lstIsModified != null)
            {
                int i = 0;
                foreach (var NombreTabla in lstIsModified)
                {
                    string json = _Reciver.GetJsonTable(NombreTabla);
                    if (json != null & json != "")
                    {
                        LoadPropiedades();
                        var client = new RestClient(routeServiceUpLoad);
                        client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                        var request = new RestRequest(Method.POST);
                        request.Resource = nameService;
                        request.RequestFormat = RestSharp.DataFormat.Json;
                        request.AddHeader("Content-type", "application/json");
                        request.AddBody(new { listPocos = json, dataUser = "", tableName = NombreTabla });
                        try
                        {
                            IRestResponse response = client.Execute(request);
                            var content = response.Content;
                            int inicio = content.IndexOf(":", 0) + 2;
                            int fin = content.IndexOf("]", inicio) + 1;
                            content = content.Substring(inicio, content.Length - (inicio + 2));
                            this.Message = "Sincronizando " + NombreTabla;
                            _Reciver._SpConfirmSincRows(content, NombreTabla);
                            this.Message = NombreTabla + " ha sido actualizada.";
                            i++;
                            responseSevice = true;
                        }
                        catch (Exception ex)
                        {
                            //responseSevice = false;
                            throw;
                        }
                    }
                }
            }
            else
            {
                this.Message = "La base de datos está actualizada.";
            }
            return responseSevice;
        }

        #endregion

        #region SyncService (Sincronización)
        public void SyncService()
        {
            this.Message = "Iniciando descarga de información...";
            try
            {
                if (this.GetStatusConexion())
                {
                    int i = int.Parse(ConfigurationManager.AppSettings["IsUpdate"]);
                    if (i == 0)
                    {

                        //valida la conexion con los servicios
                        if (this.CallServiveConexion())
                        {
                            this.ConexionServer = true;
                            this.GetValideBiging();
                        }
                        else
                        {
                            this.ConexionServer = false;
                            this.GetValideBiging();
                        }

                        this.GetVersion();
                        //-------------------

                        this.CallModifiedData();
                        this.Message = "Datos actualizados.";
                        this._Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.Message = "Refrescando datos..";
                            this._MainNotifViewModel.LoadInfo(1);
                            this._Recurrence.GetRefresh(this.NotificationRepository.LoadNotifications(this._MainNotifViewModel.Usuario));

                            this._Recurrence.GetRecurrence(this.NotificationRepository.LoadRecurrenceNotifications(this._MainNotifViewModel.Usuario));

                        }));
                        this.CallService();
                        this.Message = "Fin de la sincronización.";
                    }
                }
                else
                {
                    //valida la conexion a los servicios
                    this.ConexionServer = false;
                    this.GetValideBiging();
                    //-----------------

                    this.Message = "No hay conexión a Internet.";

                    this._Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.Message = "Refrescando datos..";
                        this._MainNotifViewModel.LoadInfo(1);
                        this._Recurrence.GetRefresh(this.NotificationRepository.LoadNotifications(this._MainNotifViewModel.Usuario));
                        this._Recurrence.GetRecurrence(this.NotificationRepository.LoadRecurrenceNotifications(this._MainNotifViewModel.Usuario));
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Message = "No hay conexión con el servidor.";

                this._Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Message = "Refrescando datos..";
                    this._MainNotifViewModel.LoadInfo(1);
                    this._Recurrence.GetRefresh(this.NotificationRepository.LoadNotifications(this._MainNotifViewModel.Usuario));
                    this._Recurrence.GetRecurrence(this.NotificationRepository.LoadRecurrenceNotifications(this._MainNotifViewModel.Usuario));
                }));
            }
            this.JobDone = true;
            SyncViewModel.IsRunning = false;
        }
        #endregion

        #region Validar conexion.
        private bool GetStatusConexion()
        {
            bool x = false;
            try
            {
                x = NetworkInterface.GetIsNetworkAvailable();
                if (!x)
                    this.Message = "No hay conexión con el servidor.";
            }
            catch (Exception)
            {
                x = false;
            }
            return x;
        }
        #endregion        

        #region Actualizar

        public bool GetVersion()
        {
            
            IsActualizando = true;
            try
            {
                ActualizadorViewModel Actualizador = new ActualizadorViewModel();
                string Path = ConfigurationManager.AppSettings["ActualizadorPath"].ToString();
                string IdAPP = ConfigurationManager.AppSettings["IdAPP"].ToString();
                string CadenaConexion = ConfigurationManager.AppSettings["ConnectionApplication"].ToString();
                string Ip = Actualizador.GetIP();
                string PcName = Environment.MachineName;
                string act = Actualizador.GetVersion(VersionAssembly, IdAPP, this.IdUsuario, Path, CadenaConexion, PcName, Ip);
                IsActualizando = false;
            }
            catch (Exception)
            {   
                ;
            }
            return true;
        }        
        
        #endregion

        #region Metodo Del Servicio para la conexion con los servicios
        public bool CallServiveConexion()
        {
            bool responseSevice = false;
            string nameService = "DoWork";

            var client = new RestClient(routeServiceValideConexion);
            client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
            var request = new RestRequest(Method.POST);
            request.Resource = nameService;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            try
            {
                IRestResponse response = client.Execute(request);
                Dictionary<string, string> resx = json.GetResponseDictionary(response.Content);
                responseSevice = json.GetDeserializeServices(resx["DoWorkResult"]);

            }
            catch (Exception)
            {
                return false;
            }
            return responseSevice;
        }
        #endregion

        #region Metodos de la canexion con los servicios

        public void GetValideBiging()
        {
            if (this.ConexionServer)
            {
                this._MainNotifViewModel.SyncViewModel.MessageConexion = "Con Conexión";
                this._MainNotifViewModel.SyncViewModel.UrlConexion = @"./img/WifiGreen.png";
                this._MainNotifViewModel.SyncViewModel.BackgroundConexion = "#31B404";
            }
            else
            {
                this._MainNotifViewModel.SyncViewModel.MessageConexion = "Sin Conexión";
                this._MainNotifViewModel.SyncViewModel.UrlConexion = @"./img/NetworkStatusNoConnection.png";
                this._MainNotifViewModel.SyncViewModel.BackgroundConexion = "#FA5858";
            }

        }

        #endregion
    }
}