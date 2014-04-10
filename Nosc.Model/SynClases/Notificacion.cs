using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Model.SynClases
{
    public class Notificacion
    {
        #region Primitive Properties

        public virtual long IdNotificacion
        {
            get;
            set;
        }

        public virtual long FechaCreacion
        {
            get;
            set;
        }

        public virtual string Parametro
        {
            get;
            set;
        }

        public virtual string Mensaje
        {
            get;
            set;
        }

        public virtual string Titulo
        {
            get;
            set;
        }

        public virtual long IdUsuario
        {
            get;
            set;
        }

        public virtual long IdApp
        {
            get;
            set;
        }

        public virtual long FechaNotificacion
        {
            get;
            set;
        }

        public virtual bool IsCerrado
        {
            get;
            set;
        }

        public virtual Nullable<long> FechaCerrado
        {
            get;
            set;
        }

        public virtual bool CanCerrar
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

        public virtual Nullable<long> ServerLastModifiedDate
        {
            get;
            set;
        }

        public virtual int Recurrencia
        {
            get;
            set;
        }

        public virtual long FechaUltimaMuestra
        {
            get;
            set;
        }

        public virtual bool IsNotificacionActiva
        {
            get;
            set;
        }

        public virtual bool IsDescartarOnClick
        {
            get;
            set;
        }

        #endregion
    }
}
