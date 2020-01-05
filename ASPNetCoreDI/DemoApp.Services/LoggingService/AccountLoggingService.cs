using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDemos.Services.LoggingService
{
    public class AccountLoggingService:IAccountLogging
    {
        public AccountLoggingService()
        {
            Debug.WriteLine("Dependency " + this.GetType().Name + " Created");

        }
        /// <inheritdoc />
        public void LogAccountAccess(int customerId, int accountId, string message)
        {
            
        }
    }
}
