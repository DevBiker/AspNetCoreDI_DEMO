using System.Collections.Generic;
using System.Linq;
using DemoApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnityDemos.Services.AccountService;
using UnityDemos.Services.Models;
using UnityDemos.Services.TransferService;

namespace UnityDemos.Web.Areas.FundTransfer.Controllers
{
    [Area("FundTransfer")]
    public class FundTransferController : BaseController
    {
        //
        // GET: /FundTransfer/FundTransfer/

        private readonly IFundTransferService _fundTransferService;
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;
        //public FundTransferController(IAccountService accountService, IFundTransferService fundTransferService, ILogger<FundTransferController> logger) : base(logger)
        //{
        //    _logger = logger;
        //    this._accountService = accountService;
        //    this._fundTransferService = fundTransferService;
        //}

        public FundTransferController(IAccountService accountService, IFundTransferService fundTransferService, ILogger<FundTransferController> logger) : base(logger)
        {
            _logger = logger;
            this._accountService = accountService;
            this._fundTransferService = fundTransferService;
        }


        //public ActionResult Index()
        //{
        //    return TryGetActionResult(() =>
        //    {
        //        var fundTransferTyoesList = _fundTransferService.GetFundTransferTypes();
        //        return View(fundTransferTyoesList);
        //    });
        //}

        public ActionResult Index()
        {
            return TryGetActionResult(() =>
            {
                var fundTransferTyoesList = _fundTransferService.GetFundTransferTypes();
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
                var currentBalance =
                    _fundTransferService.GetCurrentBalanceAfterTransfer(_accountService, amountTransferInfo);

                return Json(currentBalance);
            });
        }
    }
}