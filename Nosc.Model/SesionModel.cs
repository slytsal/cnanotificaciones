using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Model
{
    public class SesionModel:ModelBase
    {
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

        public long IdSesion
        {
            get { return _IdSesion; }
            set
            {
                if (_IdSesion != value)
                {
                    _IdSesion = value;
                    OnPropertyChanged(IdSesionPropertyName);
                }
            }
        }
        private long _IdSesion;
        public const string IdSesionPropertyName = "IdSesion";

        public bool NoCerrarSesion
        {
            get { return _NoCerrarSesion; }
            set
            {
                if (_NoCerrarSesion != value)
                {
                    _NoCerrarSesion = value;
                    OnPropertyChanged(NoCerrarSesionPropertyName);
                }
            }
        }
        private bool _NoCerrarSesion;
        public const string NoCerrarSesionPropertyName = "NoCerrarSesion";

        public UsuarioModel User
        {
            get { return _User; }
            set
            {
                if (_User != value)
                {
                    _User = value;
                    OnPropertyChanged(UserPropertyName);
                }
            }
        }
        private UsuarioModel _User;
        public const string UserPropertyName = "User";
    }
}
