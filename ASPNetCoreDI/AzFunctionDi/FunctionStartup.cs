using System;
using DemoApp.Services;
using DemoApp.Services.AccountService;
using DemoApp.Services.LoggingService;
using DemoApp.Services.RequestInfoService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(AzFunctionDi.Startup))]

namespace AzFunctionDi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddScoped<IAccountLogging, AccountLoggingService>();
            builder.Services.AddScoped<IRequestInfoService, ContextRequestInfoService>();
            builder.Services.AddHttpContextAccessor(); 

            
        }
    }
}