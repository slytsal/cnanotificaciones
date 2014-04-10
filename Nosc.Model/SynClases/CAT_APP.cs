using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Model.SynClases
{
    public class CAT_APP
    {
        #region Primitive Properties

        public virtual long IdApp
        {
            get;
            set;
        }

        public virtual string AppName
        {
            get;
            set;
        }

        public virtual string AppIcon
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

        public virtual bool IsActive
        {
            get;
            set;
        }

        #endregion
    }
}
