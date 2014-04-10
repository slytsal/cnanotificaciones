using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.Services.Dal.POCOS;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Nosc.Services.Repository.Repository
{
    public class JsonRepository
    {
        public string GetJsonDownload(string maxJson, string nombreTable)
        {
            string result = null;

            List<string> deserialize = GetDeserialize(maxJson);

            if (deserialize.Count != 0 && !string.IsNullOrEmpty(nombreTable))
                result = GetSerealizeJson(deserialize, nombreTable);
            
            return result;
        }

        private string GetSerealizeJson(List<string> deserialize, string nombreTable)
        {
            string resultJson = null;
            try
            {
                using (NoscDBEntities entity = new NoscDBEntities())
                {
                    entity.CommandTimeout = 1147483647;

                    string query = this.GetExecStoreProcedure(deserialize.First(), deserialize.Last(), nombreTable);

                    switch (nombreTable)
                    {
                        case "APP_USUARIO":
                            var result = entity.ExecuteStoreQuery<Model.SynClases.APP_USUARIO>(query, null).ToList();
                            if (result.Count > 0)
                                resultJson = SerializerJson.SerializeParametros(result);
                            break;
                        case "CAT_APP":
                            var result1 = entity.ExecuteStoreQuery<Model.SynClases.CAT_APP>(query, null).ToList();
                            if (result1.Count > 0)
                                resultJson = SerializerJson.SerializeParametros(result1);
                            break;
                        case "Notificacion":
                            var result2 = entity.ExecuteStoreQuery<Model.SynClases.Notificacion>(query, null).ToList();
                            if (result2.Count() > 0)
                                resultJson = SerializerJson.SerializeParametros(result2);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                resultJson = ex.Message;
            }
            return resultJson;
        }

        public string GetExecStoreProcedure(string lastModifiedDate, string serverLastModifiedDate, string nombreTable)
        {
            string sqlStmt;
            //Cadena que ejeucta sp
            sqlStmt = "EXEC SP_GetJsonMaxsServer "+"@lastModifiedDate = N'@@par1', "
                                                  +"@serverLastModifiedDate = N'@@par2', "
                                                  +"@tblName = N'@@par3';";

            sqlStmt = sqlStmt.Replace("@@par1", lastModifiedDate);
            sqlStmt = sqlStmt.Replace("@@par2", serverLastModifiedDate);
            sqlStmt = sqlStmt.Replace("@@par3", nombreTable);


           return sqlStmt;
        }

        public List<string> GetDeserialize(string maxJson)
        {
            List<string> result=  new List<string>();
            try
            {
                //[{LastModifiedDate:'20140305105202570', ServerLastModifiedDate:'20140305105202570'}]
                string[] stringSeparators = new string[] { "[{LastModifiedDate:'", "'", ",", "ServerLastModifiedDate:'", "}]", " ", };
                string[] resultSplit;

                resultSplit = maxJson.Split(stringSeparators, StringSplitOptions.None);

                resultSplit.Where(w => w != "").ToList().ForEach(p => result.Add(p));
            }
            catch (Exception)
            {
            }
            return result;
        }

    }
}
