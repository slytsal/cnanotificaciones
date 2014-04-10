using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace Nosc.Services.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Validacion" en el código, en svc y en el archivo de configuración a la vez.
    
    /// <summary>
    /// Solo es para validar la conexion con el servidor.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Validacion : IValidacion
    {
        public bool DoWork()
        {
            return true;
        }
    }
}
