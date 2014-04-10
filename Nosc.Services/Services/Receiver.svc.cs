using System;
using Nosc.Services.Repository.Repository;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace Nosc.Services.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Receiver : IReceiver
    {

        public string LoadCatGeneric(string listPocos, string dataUser, string tableName)
        {
            ReceiverRepository _IReciverRepository = new ReceiverRepository();
            string ids = "";
            try
            {
                ids = _IReciverRepository.GetSpUpsertServerRows(listPocos, tableName);
            }
            catch (Exception ex)
            {

                ids = ex.Message;
            }
            return ids;
        }
    }
}
