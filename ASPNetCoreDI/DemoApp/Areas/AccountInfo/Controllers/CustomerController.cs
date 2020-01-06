using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoApp.Services.CustomerService;


namespace DemoApp.Web.Areas.AccountInfo.Controllers
{
    [Area("AccountInfo")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        private readonly IHttpContextAccessor _httpContext; 
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger, IHttpContextAccessor httpContext):base(logger)
        {
            
            _logger = logger; 
            _customerService = customerService;
            _httpContext = httpContext; 
            
        }

        // GET: AccountInfo/Customer
        public ActionResult Index()
        {
            
            return TryGetActionResult(() => { return View(_customerService.GetCustomer()); });
        }
    }
}