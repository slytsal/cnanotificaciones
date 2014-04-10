using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using RestSharp;
using System.Configuration;
using Nosc.Repository;

namespace Nosc.ViewModel
{
    public class ConexionServerViewModel : ViewModelBase
    {
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

        #endregion

        #region Propiedades Servicios web
        string routeServiceValideConexion;
        string basicAuthUser;
        string basicAuthPass;
        #endregion

        #region Propiedades hilo
        public static bool IsRunning = false;
        System.Timers.Timer t;
        string _message;
        bool _jobDone;
        #endregion

        #region propiedades
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
        #endregion

        #region metodos hilo

        public void ChangesConexion(Object sender, System.Timers.ElapsedEventArgs args)
        {
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();
        }

        public void start()
        {
            this.JobDone = false;
            this.OnPropertyChanged("JobDone");
            ConexionServerViewModel.IsRunning = true;
            t.Start();
        }

        public void GetConexionViewModel()
        {
            this._jobDone = false;
            t = new System.Timers.Timer(10);
            t.Enabled = false;
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChangesConexion);

        }

        #endregion

        #region Metodo carga propiedades.
        private void LoadPropiedades()
        {
            routeServiceValideConexion = ConfigurationManager.AppSettings["ValideConexion"].ToString();
            basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
            basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();
        }
        #endregion

        #region Constructor
        public ConexionServerViewModel()
        {
            this.LoadPropiedades();
        }
        #endregion

        #region metodos de la canexion con los servicios

        public void GetValideBiging()
        {
            if (this.ConexionServer)
            {
                this.Message = "Con Conexión";
                this.UrlConexion = @"./img/WifiGreen.png";
                this.BackgroundConexion = "#31B404";
            }
            else
            {
                this.Message = "Sin Conexión";
                this.UrlConexion = @"./img/NetworkStatusNoConnection.png";
                this.BackgroundConexion = "#FA5858";
            }

        }

        public void GetStatusNetworkServices()
        {
            if (this.GetStatusConexion())
            {
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
            }
            else
            {
                this.ConexionServer = false;
                this.GetValideBiging();
            }
        }

        #endregion
        
        #region Validar conexion internet

        private bool GetStatusConexion()
        {
            bool conexion = false;
            try
            {
                conexion = NetworkInterface.GetIsNetworkAvailable();
                
            }
            catch (Exception)
            {
                return  false;
            }
            return conexion;
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
    }
}
