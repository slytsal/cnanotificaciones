using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Nosc.Repository.Sync;
using System.Configuration;
using RestSharp;
using System.Threading;
using Nosc.Repository;

namespace Nosc.ViewModel
{
    public class SplashViewModel : ViewModelBase
    {
        public Dispatcher _Dispatcher;
        public IShowWindows _ShowWindows;

        #region Instancias.
        static BroadCastRepository _BroadCast = new BroadCastRepository();
        static ReciverRepository _Reciver = new ReciverRepository();
        #endregion

        #region Propiedades Servicios web
        string routeServiceDownLoad;
        string basicAuthUser;
        string basicAuthPass;
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
            //this.JobDone = false;
            //this.OnPropertyChanged("JobDone");
            //SyncViewModel.IsRunning = true;
            //t.Start();

            Thread sync=new Thread(() => this.SyncService());
            sync.IsBackground = true;
            sync.Start();
        }
        #endregion

        #region Metodo carga propiedades.
        private void LoadPropiedades()
        {
            this.Message = "Cargando Propiedades.";

            routeServiceDownLoad = ConfigurationManager.AppSettings["BroadCast"].ToString();
            basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
            basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();
        }
        #endregion

        #region BroadCast (Descarga)
        public bool CallModifiedData()
        {
            bool responseSevice = false;
            string nameService = "GetModifiedData";
            var client = new RestClient(routeServiceDownLoad);
            client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
            var request = new RestRequest(Method.POST);
            request.Resource = nameService;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            try
            {
                this.Message = "Espere por favor..";
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                int inicio = content.IndexOf(":", 0) + 2;
                int fin = content.IndexOf("]", inicio) + 1;
                content = content.Substring(inicio, content.Length - (inicio + 2));
                List<string> lstTablas = null;
                //Se asingnar las tablas 
                lstTablas = new List<string>();
                lstTablas.Add("APP_USUARIO");
                if (lstTablas != null & lstTablas.Count > 0)
                {
                    foreach (var item in lstTablas)
                    {
                        try
                        {
                            string jsonMax =_BroadCast.GetMaxTableLocal(item);
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
            catch (Exception)
            {
            }
            return responseSevice;
        }

        public string CallGetTablesUpdate(string _tableName, string _maxJson)
        {
            string MSJ = null;
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

        #region SyncService (Sincronización)
        public void SyncService()
        {
            //Descarga de información
            try
            {
                this.CallModifiedData();
                this.Message = "Fin descarga.";

                this._Dispatcher.BeginInvoke(new Action(() =>
                {
                    this._ShowWindows.ShowAplication();
                }));
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            this.JobDone = true;
            SyncViewModel.IsRunning = false;
        }
        #endregion

        #region Constructor
        public SplashViewModel(Dispatcher dispatcher, IShowWindows showWindows)
        {
            this._Dispatcher = dispatcher;
            this._ShowWindows = showWindows;
            this.LoadPropiedades();
            this.Message = "Iniciando descarga de información...";
        }
        #endregion
    }
}
