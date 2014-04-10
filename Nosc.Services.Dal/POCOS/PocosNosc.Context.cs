﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace Nosc.Services.Dal.POCOS
{
    public partial class NoscDBEntities : ObjectContext
    {
        public const string ConnectionString = "name=NoscDBEntities";
        public const string ContainerName = "NoscDBEntities";
    
        #region Constructors
    
        public NoscDBEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public NoscDBEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public NoscDBEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<APP_USUARIO> APP_USUARIO
        {
            get { return _aPP_USUARIO  ?? (_aPP_USUARIO = CreateObjectSet<APP_USUARIO>("APP_USUARIO")); }
        }
        private ObjectSet<APP_USUARIO> _aPP_USUARIO;
    
        public ObjectSet<CAT_APP> CAT_APP
        {
            get { return _cAT_APP  ?? (_cAT_APP = CreateObjectSet<CAT_APP>("CAT_APP")); }
        }
        private ObjectSet<CAT_APP> _cAT_APP;
    
        public ObjectSet<CAT_SESION> CAT_SESION
        {
            get { return _cAT_SESION  ?? (_cAT_SESION = CreateObjectSet<CAT_SESION>("CAT_SESION")); }
        }
        private ObjectSet<CAT_SESION> _cAT_SESION;
    
        public ObjectSet<Notificacion> Notificacions
        {
            get { return _notificacions  ?? (_notificacions = CreateObjectSet<Notificacion>("Notificacions")); }
        }
        private ObjectSet<Notificacion> _notificacions;
    
        public ObjectSet<MODIFIEDDATA> MODIFIEDDATA
        {
            get { return _mODIFIEDDATA  ?? (_mODIFIEDDATA = CreateObjectSet<MODIFIEDDATA>("MODIFIEDDATA")); }
        }
        private ObjectSet<MODIFIEDDATA> _mODIFIEDDATA;
    
        public ObjectSet<SYNCTABLE> SYNCTABLE
        {
            get { return _sYNCTABLE  ?? (_sYNCTABLE = CreateObjectSet<SYNCTABLE>("SYNCTABLE")); }
        }
        private ObjectSet<SYNCTABLE> _sYNCTABLE;

        #endregion
        #region Function Imports
        public ObjectResult<string> SP_TABLE_JSON_MODIFIEDDATA(string tabla)
        {
    
            ObjectParameter tablaParameter;
    
            if (tabla != null)
            {
                tablaParameter = new ObjectParameter("Tabla", tabla);
            }
            else
            {
                tablaParameter = new ObjectParameter("Tabla", typeof(string));
            }
            return base.ExecuteFunction<string>("SP_TABLE_JSON_MODIFIEDDATA", tablaParameter);
        }
        public ObjectResult<string> SP_GetMaxServer(string jsonTable, string tblName)
        {
    
            ObjectParameter jsonTableParameter;
    
            if (jsonTable != null)
            {
                jsonTableParameter = new ObjectParameter("jsonTable", jsonTable);
            }
            else
            {
                jsonTableParameter = new ObjectParameter("jsonTable", typeof(string));
            }
    
            ObjectParameter tblNameParameter;
    
            if (tblName != null)
            {
                tblNameParameter = new ObjectParameter("tblName", tblName);
            }
            else
            {
                tblNameParameter = new ObjectParameter("tblName", typeof(string));
            }
            return base.ExecuteFunction<string>("SP_GetMaxServer", jsonTableParameter, tblNameParameter);
        }
        public ObjectResult<string> SpUpsertServerRows(string json, string nomTbl)
        {
    
            ObjectParameter jsonParameter;
    
            if (json != null)
            {
                jsonParameter = new ObjectParameter("json", json);
            }
            else
            {
                jsonParameter = new ObjectParameter("json", typeof(string));
            }
    
            ObjectParameter nomTblParameter;
    
            if (nomTbl != null)
            {
                nomTblParameter = new ObjectParameter("nomTbl", nomTbl);
            }
            else
            {
                nomTblParameter = new ObjectParameter("nomTbl", typeof(string));
            }
            return base.ExecuteFunction<string>("SpUpsertServerRows", jsonParameter, nomTblParameter);
        }
        public ObjectResult<SP_GetJsonMaxsServer_Result> SP_GetJsonMaxsServer(string lastModifiedDate, string serverLastModifiedDate, string tblName)
        {
    
            ObjectParameter lastModifiedDateParameter;
    
            if (lastModifiedDate != null)
            {
                lastModifiedDateParameter = new ObjectParameter("lastModifiedDate", lastModifiedDate);
            }
            else
            {
                lastModifiedDateParameter = new ObjectParameter("lastModifiedDate", typeof(string));
            }
    
            ObjectParameter serverLastModifiedDateParameter;
    
            if (serverLastModifiedDate != null)
            {
                serverLastModifiedDateParameter = new ObjectParameter("serverLastModifiedDate", serverLastModifiedDate);
            }
            else
            {
                serverLastModifiedDateParameter = new ObjectParameter("serverLastModifiedDate", typeof(string));
            }
    
            ObjectParameter tblNameParameter;
    
            if (tblName != null)
            {
                tblNameParameter = new ObjectParameter("tblName", tblName);
            }
            else
            {
                tblNameParameter = new ObjectParameter("tblName", typeof(string));
            }
            return base.ExecuteFunction<SP_GetJsonMaxsServer_Result>("SP_GetJsonMaxsServer", lastModifiedDateParameter, serverLastModifiedDateParameter, tblNameParameter);
        }

        #endregion
    }
}
