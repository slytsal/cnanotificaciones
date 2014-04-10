using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.DAL;

namespace Nosc.Repository.Sync
{
    public class BroadCastRepository
    {
        #region Local.

        /// <summary>
        /// Metodo que manda a llamar el procedimiento almacenado "SP_GetMax".
        /// </summary>
        /// <param name="tableName">Nombre de una tabla</param>
        /// <returns>Retorna una cadena de string en formato JSON con el registro maximo de una tabla.</returns>
        public string GetMaxTableLocal(string tableName)
        {
            string res = "";

            using (var entity = new NoscDBEntities())
            {
                res = entity.SP_GetMax(tableName).First<string>();
            }

            return res;
        }


        /// <summary>
        /// Metodo que manda a llamar el procedimiento almacenado "SP_GetTableNameModifiedData".
        /// </summary>
        /// <param name="jsonModifiedServer"></param>
        /// <returns>Retorna una lista de Strings con los nombres de las tablas que se van a sincronizar</returns>
        public List<string> GetTableNameModifiedData(string jsonModifiedServer)
        {
            List<string> lst = null;
            using (var entity = new NoscDBEntities())
            {
                lst = entity.SP_GetTableNameModifiedData(jsonModifiedServer).ToList<string>();
            }

            return lst;
        }

        /// <summary>
        /// Metodo que manda a llamar el procedimiento almacenado "SpUpsertServerRows".
        /// </summary>
        /// <param name="jsonTable">Registros de la tabla en formato JSON que se van a insertar o actualizar.</param>
        /// <param name="tableName">Nombre de una tabla.</param>
        /// <returns>Retorna el Id de los registros que se insertaron o actualizaron </returns>
        public string SpUpsertServerRows(string jsonTable, string tableName)
        {
            string res = "";

            using (var entity = new NoscDBEntities())
            {
                res = entity.SP_Upsert_Server_Rows(jsonTable, tableName).First<string>();

            }

            return res;
        }


        /// <summary>
        /// Metodo que manda a llamar el procedimiento almacenado "SP_UpdateModifiedDataLocal".
        /// </summary>
        /// <param name="nomTbl">Nombre de una tabla.</param>
        /// <param name="jsonTable">Registros de una tabla en formato JSON.</param>
        /// <returns>Retorna un mensaje de "OK"</returns>
        public string SP_UpdateModifiedDataLocal(string nomTbl, string jsonTable)
        {
            string res = "";
            using (var entity = new NoscDBEntities())
            {
                res = entity.SP_UpdateModifiedDataLocal(nomTbl, jsonTable).First<string>();
            }
            return res;
        }
        #endregion
    }
}
