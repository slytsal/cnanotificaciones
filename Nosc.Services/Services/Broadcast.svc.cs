using System;
using Nosc.Services.Repository.Repository;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Nosc.Services.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Broadcast" en el código, en svc y en el archivo de configuración a la vez.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Broadcast : IBroadcast
    {

        public string GetModifiedData()
        {
            string ModifiedData = "";
            BroadcastRepository _BroadCastRepository = new BroadcastRepository();
            try
            {
                ModifiedData = _BroadCastRepository.GetModifiedData();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ModifiedData;
        }

        public string GetTablesUpdate(string tableName, string maxJson)
        {
            string res = "";
            BroadcastRepository _BroadCastRepository = new BroadcastRepository();
            try
            {
                res = _BroadCastRepository.GetTablesUpdate(tableName, maxJson);
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }
    }
}
