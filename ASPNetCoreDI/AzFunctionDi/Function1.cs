using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DemoApp.Services.AccountService;

namespace MyNamespace
{
    public class HttpTrigger
    {
        private readonly IAccountService _service;

        public HttpTrigger(IAccountService service)
        {
            _service = service;
        }

        [FunctionName("GetAccount")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "posts")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var res = _service.GetAllAccountInfo(2);

            return new JsonResult(res);
        }
    }
}