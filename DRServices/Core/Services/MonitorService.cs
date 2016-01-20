using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DRServices.Core.Services
{
    public class MonitorService : DRServices.Core.Interfaces.MonitorInterface
    {
        private Database _SecurityDB;

        public MonitorService()
        {
            _SecurityDB = DatabaseFactory.CreateDatabase();
        }

        
        string DRServices.Core.Interfaces.MonitorInterface.GetEXP()
        {
            return "Worked";
        }
    }
}