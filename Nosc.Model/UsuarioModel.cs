using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Model
{
    public class UsuarioModel : ModelBase
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

        public string UsuarioCorreo
        {
            get { return _UsuarioCorreo; }
            set
            {
                if (_UsuarioCorreo != value)
                {
                    _UsuarioCorreo = value;
                    OnPropertyChanged(UsuarioCorreoPropertyName);
                }
            }
        }
        private string _UsuarioCorreo;
        public const string UsuarioCorreoPropertyName = "UsuarioCorreo";

        public string UsuarioPwd
        {
            get { return _UsuarioPwd; }
            set
            {
                if (_UsuarioPwd != value)
                {
                    _UsuarioPwd = value;
                    OnPropertyChanged(UsuarioPwdPropertyName);
                }
            }
        }
        private string _UsuarioPwd;
        public const string UsuarioPwdPropertyName = "UsuarioPwd";

        public string NombreUsuario
        {
            get { return _NombreUsuario; }
            set
            {
                if (_NombreUsuario != value)
                {
                    _NombreUsuario = value;
                    OnPropertyChanged(NombreUsuarioPropertyName);
                }
            }
        }
        private string _NombreUsuario;
        public const string NombreUsuarioPropertyName = "NombreUsuario";

        public string Apellido
        {
            get { return _Apellido; }
            set
            {
                if (_Apellido != value)
                {
                    _Apellido = value;
                    OnPropertyChanged(ApellidoPropertyName);
                }
            }
        }
        private string _Apellido;
        public const string ApellidoPropertyName = "Apellido";

        public string Area
        {
            get { return _Area; }
            set
            {
                if (_Area != value)
                {
                    _Area = value;
                    OnPropertyChanged(AreaPropertyName);
                }
            }
        }
        private string _Area;
        public const string AreaPropertyName = "Area";

        public string Puesto
        {
            get { return _Puesto; }
            set
            {
                if (_Puesto != value)
                {
                    _Puesto = value;
                    OnPropertyChanged(PuestoPropertyName);
                }
            }
        }
        private string _Puesto;
        public const string PuestoPropertyName = "Puesto";

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

        public bool IsNewUser
        {
            get { return _IsNewUser; }
            set
            {
                if (_IsNewUser != value)
                {
                    _IsNewUser = value;
                    OnPropertyChanged(IsNewUserPropertyName);
                }
            }
        }
        private bool _IsNewUser;
        public const string IsNewUserPropertyName = "IsNewUser";

        public bool IsVerified
        {
            get { return _IsVerified; }
            set
            {
                if (_IsVerified != value)
                {
                    _IsVerified = value;
                    OnPropertyChanged(IsVerifiedPropertyName);
                }
            }
        }
        private bool _IsVerified;
        public const string IsVerifiedPropertyName = "IsVerified";

        public Nullable<bool> IsMailSent
        {
            get { return _IsMailSent; }
            set
            {
                if (_IsMailSent != value)
                {
                    _IsMailSent = value;
                    OnPropertyChanged(IsMailSentPropertyName);
                }
            }
        }
        private Nullable<bool> _IsMailSent;
        public const string IsMailSentPropertyName = "IsMailSent";

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

    }
}
