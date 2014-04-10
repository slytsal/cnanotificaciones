using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nosc.Model
{
    public class AppModel:ModelBase,IEquatable<AppModel>
    {
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

        public string AppName
        {
            get { return _AppName; }
            set
            {
                if (_AppName != value)
                {
                    _AppName = value;
                    OnPropertyChanged(AppNamePropertyName);
                }
            }
        }
        private string _AppName;
        public const string AppNamePropertyName = "AppName";

        public string AppIconPath
        {
            get { return _AppIconPath; }
            set
            {
                if (_AppIconPath != value)
                {
                    _AppIconPath = value;
                    OnPropertyChanged(AppIconPathPropertyName);
                }
            }
        }
        private string _AppIconPath;
        public const string AppIconPathPropertyName = "AppIconPath";

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

        public bool Equals(AppModel obj)
        {

            return (
                (obj as AppModel)!=null? this.IdApp == ((AppModel)obj).IdApp : false 
            );
        }

        public override int GetHashCode()
        {
            return this.IdApp.GetHashCode();
        }
    }
}
