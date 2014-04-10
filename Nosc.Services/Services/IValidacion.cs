using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace Nosc.Services.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IValidacion" en el código y en el archivo de configuración a la vez.
    /// <summary>
    /// Solo es para validar la conexion con el servidor.
    /// </summary>
    [ServiceContract]
    public interface IValidacion
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        bool DoWork();
    }
}
