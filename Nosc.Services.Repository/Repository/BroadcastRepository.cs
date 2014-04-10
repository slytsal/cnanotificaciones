using System;
using System.Linq;
using Nosc.Services.Dal.POCOS;

namespace Nosc.Services.Repository.Repository
{
    public class BroadcastRepository
    {
        public string GetModifiedData()
        {
            string res = "";
            try
            {
                using (var entity = new NoscDBEntities())
                {
                    res = entity.SP_TABLE_JSON_MODIFIEDDATA("MODIFIEDDATA").First<string>();
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        public string GetTablesUpdate(string tableName, string maxJson)
        {
            JsonRepository json = new JsonRepository();
            string res = "";
            try
            {
                res = json.GetJsonDownload(maxJson, tableName);
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }
    }
}
