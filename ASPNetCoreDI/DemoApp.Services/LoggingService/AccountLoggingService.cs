using DemoApp.Services.RequestInfoService;
using Microsoft.Extensions.Configuration;
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
        public AccountLoggingService(IRequestInfoService requestInfoService, IConfiguration config)
        {
            _requestInfoService = requestInfoService ?? throw new ArgumentException(nameof(requestInfoService));
            _config = config ?? throw new ArgumentException(nameof(config));
            Debug.WriteLine("*** Dependency " + this.GetType().Name + " Created");

        }
        /// <inheritdoc />
        public void LogAccountAccess(int customerId, int accountId, string message)
        {
            //
        }
    }
}
