using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Capstone
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IExpiryEstimationService" in both code and config file together.
    [ServiceContract]
    public interface IExpiryEstimationService
    {
        [OperationContract]
        DateTime DoWork(string productName);
    }
}
