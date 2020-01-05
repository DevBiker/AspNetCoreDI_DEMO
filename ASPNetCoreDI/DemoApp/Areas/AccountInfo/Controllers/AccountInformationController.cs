using System;
using System.Collections.Generic;
using System.Linq;
using DemoApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnityDemos.Services.AccountService;
using UnityDemos.Services.CustomerService;
using UnityDemos.Services.Models;

namespace UnityDemos.Web.Areas.AccountInfo.Controllers
{
    [Area("AccountInfo")]
    public class AccountInformationController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ILogger<AccountInformationController> _logger; 
        public AccountInformationController(IAccountService accountService, ICustomerService customerService, ILogger<AccountInformationController> logger):base(logger)
        {
            _logger = logger; 
            this._accountService = accountService;
            _customerService = customerService;
        }
        public ActionResult Index()
        {
            return TryGetActionResult(() =>
            {
                List<Account> allAccounts = _accountService.GetAllAccountInfo(111).ToList();
                return View(allAccounts);
            });
        }
        public ActionResult GetAccountDetail(string accountNumber)
        {
            return TryGetActionResult(() =>
            {
                Account accountDetail = _accountService.GetAccountDetail(Convert.ToInt32(accountNumber));
                return Json(accountDetail);
            });
        }
    }
}