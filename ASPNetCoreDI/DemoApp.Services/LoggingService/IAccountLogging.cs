using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDemos.Services.LoggingService
{
    public interface IAccountLogging
    {
        void LogAccountAccess(int customerId, int accountId, string message);

    }
}
