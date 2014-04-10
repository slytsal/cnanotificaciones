using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.DAL;

namespace Nosc.Repository.Sync
{
    public class ReciverRepository
    {
        #region Local
        public List<string> GetIsModified()
        {

            List<string> lst = null;
            using (var entity = new NoscDBEntities())
            {
                lst = new List<string>();
                (from md in entity.MODIFIEDDATA
                 join s in entity.SYNCTABLE on md.IdSyncTable equals s.IdSyncTable
                 where md.IsModified == true
                 orderby s.OrderTable ascending
                 select new { s.SyncTableName }).ToList().ForEach(row =>
                                  lst.Add(row.SyncTableName)
                                  );

                if (lst.Count > 0)
                {
                    return lst;
                }
                else
                {
                    return null;
                }

            }


        }

        public string GetJsonTable(string tableName)
        {

            using (var entity = new NoscDBEntities())
            {
                string json = entity.SP_TABLE_JSON(tableName).First<string>();
                return json;
            }

        }

        public bool _SpConfirmSincRows(string json, string tableName)
        {
            bool x = false;

            using (var entity = new NoscDBEntities())
            {
                entity.SpConfirmSincRows(json, tableName);
                return x = true;
            }

        }
        #endregion
    }
}
