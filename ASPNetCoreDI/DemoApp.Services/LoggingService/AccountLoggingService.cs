using DemoApp.Services.RequestInfoService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoApp.Services.LoggingService
{
    public class AccountLoggingService:IAccountLogging
    {

    
        ILogger _logger; 
        public AccountLoggingService( ILogger logger)
        {
            
     
            Debug.WriteLine("*** Dependency " + this.GetType().Name + " Created");
            _logger = logger; 

        }
        /// <inheritdoc />
        public void LogAccountAccess(int customerId, int accountId, string message)
        {
            //_logger.LogInformation("Customer {0} accessed account {1}", customerId, accountId);
        }
    }
}
