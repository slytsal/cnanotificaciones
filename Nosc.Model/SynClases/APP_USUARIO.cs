using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Model.SynClases
{
    public class APP_USUARIO
    {
        #region Primitive Properties

        public virtual long IdUsuario
        {
            get;
            set;
        }

        public virtual string UsuarioCorreo
        {
            get;
            set;
        }

        public virtual string UsuarioPwd
        {
            get;
            set;
        }

        public virtual string Nombre
        {
            get;
            set;
        }

        public virtual string Apellido
        {
            get;
            set;
        }

        public virtual string Area
        {
            get;
            set;
        }

        public virtual string Puesto
        {
            get;
            set;
        }

        public virtual bool IsActive
        {
            get;
            set;
        }

        public virtual bool IsModified
        {
            get;
            set;
        }

        public virtual long LastModifiedDate
        {
            get;
            set;
        }

        public virtual bool IsNewUser
        {
            get;
            set;
        }

        public virtual bool IsVerified
        {
            get;
            set;
        }

        public virtual Nullable<bool> IsMailSent
        {
            get;
            set;
        }

        public virtual Nullable<long> ServerLastModifiedDate
        {
            get;
            set;
        }

        #endregion
    }
}
