using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.Model;
using Nosc.Repository;

namespace Nosc.ViewModel
{
    public class UserLoginViewModel:ViewModelBase
    {
        public RelayCommand LoginCommand
        {
            get 
            {
                return _LoginCommand;
            }
        }
        private RelayCommand _LoginCommand;
        public const string LoginCommandPropertyName = "LoginCommand";

        public SesionRepository SesionRepository
        {
            get { return _SesionRepository; }
            set
            {
                if (_SesionRepository != value)
                {
                    _SesionRepository = value;
                    OnPropertyChanged(SesionRepositoryPropertyName);
                }
            }
        }
        private SesionRepository _SesionRepository;
        public const string SesionRepositoryPropertyName = "SesionRepository";

        public UserRepository UserRepository
        {
            get { return _UserRepository; }
            set
            {
                if (_UserRepository != value)
                {
                    _UserRepository = value;
                    OnPropertyChanged(UserRepositoryPropertyName);
                }
            }
        }
        private UserRepository _UserRepository;
        public const string UserRepositoryPropertyName = "UserRepository";

        public SesionModel Sesion
        {
            get { return _Sesion; }
            set
            {
                if (_Sesion != value)
                {
                    _Sesion = value;
                    OnPropertyChanged(SesionPropertyName);
                }
            }
        }
        private SesionModel _Sesion;
        public const string SesionPropertyName = "Sesion";

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

        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged(UserNamePropertyName);
                }
            }
        }
        private string _UserName;
        public const string UserNamePropertyName = "UserName";

        public string UserPassword
        {
            get { return _UserPassword; }
            set
            {
                if (_UserPassword != value)
                {
                    _UserPassword = value;
                    OnPropertyChanged(UserPasswordPropertyName);
                }
            }
        }
        private string _UserPassword;
        public const string UserPasswordPropertyName = "UserPassword";

        public bool CerrarSesion
        {
            get { return _CerrarSesion; }
            set
            {
                if (_CerrarSesion != value)
                {
                    _CerrarSesion = value;
                    OnPropertyChanged(CerrarSesionPropertyName);
                }
            }
        }
        private bool _CerrarSesion;
        public const string CerrarSesionPropertyName = "CerrarSesion";

        public string StatusMsg
        {
            get { return _StatusMsg; }
            set
            {
                if (_StatusMsg != value)
                {
                    _StatusMsg = value;
                    OnPropertyChanged(StatusMsgPropertyName);
                }
            }
        }
        private string _StatusMsg;
        public const string StatusMsgPropertyName = "StatusMsg";

        //Methods
        public void GetSesion()
        {
            this.Sesion = this.SesionRepository.GetSesion();
        }

        public bool CanLogin()
        {
            bool canLogin = false;

            canLogin = !String.IsNullOrEmpty(this.UserName) && !String.IsNullOrEmpty(this.UserPassword);

            return canLogin;
        }

        public void Login()
        {
            this.StatusMsg = "";
            this.User = this.UserRepository.GetUsuario(this.UserName, this.UserPassword);

            if (this.User != null)
            {
                if (this.User.IsActive)
                {
                    this.Sesion = new SesionModel()
                    {
                        NoCerrarSesion = this.CerrarSesion,
                        User = this.User
                    };

                    this.SesionRepository.SetSesion(this.Sesion);
                }
                else
                {
                    this.StatusMsg = "Usuario Desactivado";
                    this.User = null;
                }
            }
            else
                this.StatusMsg = "Nombre de usuario o contraseña incorrectos";
        }

        public bool UserSet()
        {
            return this.User != null;
        }

        //Constructor
        public UserLoginViewModel()
        {
            _LoginCommand = new RelayCommand(a => this.Login(), b => this.CanLogin());

            this.SesionRepository = new SesionRepository();
            this.UserRepository = new UserRepository();

            //Obtener información de la ultima sesion
            this.GetSesion();

            if (this.Sesion != null && this.Sesion.User.IsActive)
            {
                this.UserName = this.Sesion.User.UsuarioCorreo;
                this.CerrarSesion = this.Sesion.NoCerrarSesion;

                //Si no cierra sesión obtener ya el usuario
                if (this.CerrarSesion)
                {
                    UsuarioModel u = this.UserRepository.GetUsuario(this.Sesion.User.UsuarioCorreo);
                    if (u != null)
                        this.User = u;
                }
            }
            else
                this.StatusMsg = "Usuario Desactivado";
        }
    }
}
