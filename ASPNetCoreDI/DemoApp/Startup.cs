using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UnityDemos.Services.AccountService;
using UnityDemos.Services.CustomerService;
using UnityDemos.Services.LoggingService;
using UnityDemos.Services.Models;
using UnityDemos.Services.TransferService;

namespace DemoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddTransient<IAccountService>(
                    provider => new AccountService(provider.GetService<IAccountLogging>()))
                .AddTransient<ICustomerService, CustomerService>()
                //The account logging service is used by multiple other dependencies in a call. 
                .AddTransient<IAccountLogging, AccountLoggingService>()
                .TryAddEnumerable(new[] {
                    ServiceDescriptor.Transient<IFundTransferService, InterBankTransferService>(),
                    ServiceDescriptor.Transient<IFundTransferService, IntraBankTransferService>(),
                    ServiceDescriptor.Transient<IFundTransferService, OwnAccountTransferService>(),
                    });
                

            services.AddLogging();
            services.AddHttpContextAccessor();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //routes.MapAreaRoute(name: "AccountInfo", areaName: "AccountInfo", template: "AccountInfo/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "Areas", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
