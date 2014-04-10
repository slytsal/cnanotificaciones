//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Nosc.Services.Dal.POCOS
{
    public partial class CAT_APP
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
        #region Navigation Properties
    
        public virtual ICollection<Notificacion> Notificacions
        {
            get
            {
                if (_notificacions == null)
                {
                    var newCollection = new FixupCollection<Notificacion>();
                    newCollection.CollectionChanged += FixupNotificacions;
                    _notificacions = newCollection;
                }
                return _notificacions;
            }
            set
            {
                if (!ReferenceEquals(_notificacions, value))
                {
                    var previousValue = _notificacions as FixupCollection<Notificacion>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupNotificacions;
                    }
                    _notificacions = value;
                    var newValue = value as FixupCollection<Notificacion>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupNotificacions;
                    }
                }
            }
        }
        private ICollection<Notificacion> _notificacions;

        #endregion
        #region Association Fixup
    
        private void FixupNotificacions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Notificacion item in e.NewItems)
                {
                    item.CAT_APP = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Notificacion item in e.OldItems)
                {
                    if (ReferenceEquals(item.CAT_APP, this))
                    {
                        item.CAT_APP = null;
                    }
                }
            }
        }

        #endregion
    }
}
