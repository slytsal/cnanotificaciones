using System;
using System.Linq;
using Nosc.Services.Dal.POCOS;

namespace Nosc.Services.Repository.Repository
{
    public class ReceiverRepository
    {
        public string GetSpUpsertServerRows(string json, string tableName)
        {
            try
            {
                using (var entity = new NoscDBEntities())
                {
                    string ids = entity.SpUpsertServerRows(json, tableName).First<string>();
                    return ids;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
