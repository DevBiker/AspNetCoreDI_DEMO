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
        IRequestInfoService _requestInfoService;
        IConfiguration _config;
        ILogger _logger; 
        public AccountLoggingService(IRequestInfoService requestInfoService, IConfiguration config, ILogger logger)
        {
            _requestInfoService = requestInfoService ?? throw new ArgumentException(nameof(requestInfoService));
            _config = config ?? throw new ArgumentException(nameof(config));
            Debug.WriteLine("*** Dependency " + this.GetType().Name + " Created");
            _logger = logger; 

        }
        /// <inheritdoc />
        public void LogAccountAccess(int customerId, int accountId, string message)
        {
            _logger.LogInformation("Customer {0} accessed account {1} from IP Address {2}", customerId, accountId, _requestInfoService.IpAddress);
        }
    }
}
