using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoApp.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ILogger _logger; 
        public BaseController(ILogger logger)
        {
            _logger = logger; 
        }
        protected TResult TryGetActionResult<TResult>(Func<TResult> action, [CallerMemberName] string callerMemberName = null)
            where TResult: IActionResult
        {
            try
            {
                _logger.LogInformation("Start GetActionResult :: " + callerMemberName);
                return action.Invoke();
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception in GetActionResult :: {0} - {1} : {2}", callerMemberName, exception.GetType().Name, exception.Message);
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                _logger.LogInformation("Exiting GetActionResult :: " + callerMemberName);
            }
        }
    }
}