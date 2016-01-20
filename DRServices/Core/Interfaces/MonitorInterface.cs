using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DRServices.Core.Interfaces
{
    [ServiceContract]
    public interface MonitorInterface
    {
        [OperationContract]
        string GetEXP();
    }
}
