using System.Collections.Generic;
using System.Linq;
using DemoApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoApp.Services.AccountService;
using DemoApp.Services.LoggingService;
using DemoApp.Services.Models;
using DemoApp.Services.TransferService;

namespace DemoApp.Web.Areas.FundTransfer.Controllers
{
    [Area("FundTransfer")]
    public class FundTransferController : BaseController
    {
        //
        // GET: /FundTransfer/FundTransfer/

        private readonly IFundTransferService[] _fundTransferService;
        private readonly IAccountService _accountService;
        private readonly IAccountLogging _accountLogging;
        private readonly ILogger _logger;

        public FundTransferController(IAccountService accountService, IEnumerable<IFundTransferService> fundTransferService, 
            IAccountLogging accountLogging,  ILogger<FundTransferController> logger) : base(logger)
        {
            _logger = logger;
            this._accountService = accountService;
            this._accountLogging = accountLogging; 
            this._fundTransferService = fundTransferService.ToArray();
        }



        public ActionResult Index()
        {
            return TryGetActionResult(() =>
            {
                var fundTransferTyoesList = _fundTransferService[0].GetFundTransferTypes();
                return View(fundTransferTyoesList);
            });
        }



        public ActionResult GetTransactionType(string selectedTransferType)
        {
            return TryGetActionResult(() =>
            {
                var accountNumberList = _accountService.GetAccountNumber(0);

                return Json(accountNumberList);
            });
        }

        //public ActionResult GetCurrentBalanceAfterTransfer(Transaction amountTransferInfo)
        //{
        //    return TryGetActionResult(() =>
        //    {
        //        var currentBalance =
        //            _fundTransferService.GetCurrentBalanceAfterTransfer(_accountService, amountTransferInfo);

        //        return Json(currentBalance);
        //    });
        //}

        public ActionResult GetCurrentBalanceAfterTransfer(Transaction amountTransferInfo)
        {
            return TryGetActionResult(() =>
            {
                _accountLogging.LogAccountAccess(amountTransferInfo.CustomerId, amountTransferInfo.FromAccount, "Transfer From Starting");
                //Get the transfer service. 
                var serviceImpl = _fundTransferService.First(e => e.HandlesTransactionType(amountTransferInfo.TypeOfTransaction));
                var currentBalance =
                    serviceImpl.GetCurrentBalanceAfterTransfer(_accountService, amountTransferInfo);

                return Json(currentBalance);
            });
        }
    }
}