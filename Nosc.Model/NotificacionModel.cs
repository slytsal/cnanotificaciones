using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nosc.Model
{
    public class NotificacionModel:ModelBase
    {
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

        public long FechaCreacion
        {
            get { return _FechaCreacion; }
            set
            {
                if (_FechaCreacion != value)
                {
                    _FechaCreacion = value;
                    OnPropertyChanged(FechaCreacionPropertyName);
                }
            }
        }
        private long _FechaCreacion;
        public const string FechaCreacionPropertyName = "FechaCreacion";

        public string Parametro
        {
            get { return _Parametro; }
            set
            {
                if (_Parametro != value)
                {
                    _Parametro = value;
                    OnPropertyChanged(ParametroPropertyName);
                }
            }
        }
        private string _Parametro;
        public const string ParametroPropertyName = "Parametro";

        public string Mensaje
        {
            get { return _Mensaje; }
            set
            {
                if (_Mensaje != value)
                {
                    _Mensaje = value;
                    OnPropertyChanged(MensajePropertyName);
                }
            }
        }
        private string _Mensaje;
        public const string MensajePropertyName = "Mensaje";

        public string Titulo
        {
            get { return _Titulo; }
            set
            {
                if (_Titulo != value)
                {
                    _Titulo = value;
                    OnPropertyChanged(TituloPropertyName);
                }
            }
        }
        private string _Titulo;
        public const string TituloPropertyName = "Titulo";

        public long IdUsuario
        {
            get { return _IdUsuario; }
            set
            {
                if (_IdUsuario != value)
                {
                    _IdUsuario = value;
                    OnPropertyChanged(IdUsuarioPropertyName);
                }
            }
        }
        private long _IdUsuario;
        public const string IdUsuarioPropertyName = "IdUsuario";

        public long IdApp
        {
            get { return _IdApp; }
            set
            {
                if (_IdApp != value)
                {
                    _IdApp = value;
                    OnPropertyChanged(IdAppPropertyName);
                }
            }
        }
        private long _IdApp;
        public const string IdAppPropertyName = "IdApp";

        public long FechaNotificacion
        {
            get { return _FechaNotificacion; }
            set
            {
                if (_FechaNotificacion != value)
                {
                    _FechaNotificacion = value;
                    this.CoverStringFechaNotificacion();
                    OnPropertyChanged(FechaNotificacionPropertyName);
                }
            }
        }
        private long _FechaNotificacion;
        public const string FechaNotificacionPropertyName = "FechaNotificacion";

        public bool IsCerrado
        {
            get { return _IsCerrado; }
            set
            {
                if (_IsCerrado != value)
                {
                    _IsCerrado = value;
                    OnPropertyChanged(IsCerradoPropertyName);
                }
            }
        }
        private bool _IsCerrado;
        public const string IsCerradoPropertyName = "IsCerrado";

        public Nullable<long> FechaCerrado
        {
            get { return _FechaCerrado; }
            set
            {
                if (_FechaCerrado != value)
                {
                    _FechaCerrado = value;
                    OnPropertyChanged(FechaCerradoPropertyName);
                }
            }
        }
        private Nullable<long> _FechaCerrado;
        public const string FechaCerradoPropertyName = "FechaCerrado";

        public bool CanCerrar
        {
            get { return _CanCerrar; }
            set
            {
                if (_CanCerrar != value)
                {
                    _CanCerrar = value;
                    OnPropertyChanged(CanCerrarPropertyName);
                }
            }
        }
        private bool _CanCerrar;
        public const string CanCerrarPropertyName = "CanCerrar";

        public long LastModifiedDate
        {
            get { return _LastModifiedDate; }
            set
            {
                if (_LastModifiedDate != value)
                {
                    _LastModifiedDate = value;
                    OnPropertyChanged(LastModifiedDatePropertyName);
                }
            }
        }
        private long _LastModifiedDate;
        public const string LastModifiedDatePropertyName = "LastModifiedDate";

        public bool IsModified
        {
            get { return _IsModified; }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                    OnPropertyChanged(IsModifiedPropertyName);
                }
            }
        }
        private bool _IsModified;
        public const string IsModifiedPropertyName = "IsModified";

        public Nullable<long> ServerLastModifiedDate
        {
            get { return _ServerLastModifiedDate; }
            set
            {
                if (_ServerLastModifiedDate != value)
                {
                    _ServerLastModifiedDate = value;
                    OnPropertyChanged(ServerLastModifiedDatePropertyName);
                }
            }
        }
        private Nullable<long> _ServerLastModifiedDate;
        public const string ServerLastModifiedDatePropertyName = "ServerLastModifiedDate";

        public int Recurrencia
        {
            get { return _Recurrencia; }
            set
            {
                if (_Recurrencia != value)
                {
                    _Recurrencia = value;
                    OnPropertyChanged(RecurrenciaPropertyName);
                }
            }
        }
        private int _Recurrencia;
        public const string RecurrenciaPropertyName = "Recurrencia";

        public long FechaUltimaMuestra
        {
            get { return _FechaUltimaMuestra; }
            set
            {
                if (_FechaUltimaMuestra != value)
                {
                    _FechaUltimaMuestra = value;
                    OnPropertyChanged(FechaUltimaMuestraPropertyName);
                }
            }
        }
        private long _FechaUltimaMuestra;
        public const string FechaUltimaMuestraPropertyName = "FechaUltimaMuestra";

        public bool IsNotificacionActiva
        {
            get { return _IsNotificacionActiva; }
            set
            {
                if (_IsNotificacionActiva != value)
                {
                    _IsNotificacionActiva = value;
                    OnPropertyChanged(IsNotificacionActivaPropertyName);
                }
            }
        }
        private bool _IsNotificacionActiva;
        public const string IsNotificacionActivaPropertyName = "IsNotificacionActiva";

        public bool IsDescartarOnClick
        {
            get { return _IsDescartarOnClick; }
            set
            {
                if (_IsDescartarOnClick != value)
                {
                    _IsDescartarOnClick = value;
                    OnPropertyChanged(IsDescartarOnClickPropertyName);
                }
            }
        }
        private bool _IsDescartarOnClick;
        public const string IsDescartarOnClickPropertyName = "IsDescartarOnClick";

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


        public string FechaDiaNotificacion
        {
            get { return _FechaDiaNotificacion; }
            set
            {
                if (_FechaDiaNotificacion != value)
                {
                    _FechaDiaNotificacion = value;
                    OnPropertyChanged(SignatariosPropertyName);
                }
            }
        }
        private string _FechaDiaNotificacion;
        public const string SignatariosPropertyName = "FechaDiaNotificacion";


        public void CoverStringFechaNotificacion()
        {
            //fecha acutual
            DateTime fechaActual = DateTime.Now;
            //fecha de notificación
            string fechaNotifi = this.FechaNotificacion.ToString().Substring(0, 8);

            int ano = int.Parse( fechaNotifi.Substring(0, 4));
            int mes = int.Parse( fechaNotifi.Substring(4, 2));
            int dia = int.Parse(fechaNotifi.Substring(6, 2));

            DateTime fechaNotificacion = new DateTime(ano, mes, dia);
            // Diferencia en dias 
            TimeSpan ts = fechaActual - fechaNotificacion;

            int resultado = ts.Days;

            if (resultado == 1 || resultado == 0)
                this.FechaDiaNotificacion = "Hace "+ resultado + " Dia";
            else
                this.FechaDiaNotificacion = "Hace "+ resultado + " Dias";
        }

        public IEnumerable<NotificacionActivaModel> NotificacionActiva
        {
            get { return _NotificacionActiva; }
            set
            {
                if (_NotificacionActiva != value)
                {
                    _NotificacionActiva = value;
                    OnPropertyChanged(NotificacionActivaPropertyName);
                }
            }
        }
        private IEnumerable<NotificacionActivaModel> _NotificacionActiva;
        public const string NotificacionActivaPropertyName = "NotificacionActiva";

    }
}
