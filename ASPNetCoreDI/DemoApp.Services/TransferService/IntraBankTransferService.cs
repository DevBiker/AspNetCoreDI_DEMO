using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DemoApp.Services.AccountService;
using DemoApp.Services.LoggingService;
using DemoApp.Services.Models;

namespace DemoApp.Services.TransferService
{
    public class IntraBankTransferService : IFundTransferService
    {
        IAccountService _accountService;
        IAccountLogging _accountLogging;

        public IntraBankTransferService(IAccountService accountService, IAccountLogging accountLogging)
        {
            _accountService = accountService;
            _accountLogging = accountLogging;
            Debug.WriteLine("*** Dependency " + this.GetType().Name + " Created");
        }

        public bool SaveWithinCustomerAccountTransaction( Transaction transaction)
        {
            return true;
        }




        public List<TransactionType> GetFundTransferTypes()
        {
            return Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().ToList();
        }




        public double GetCurrentBalanceAfterTransfer( Transaction accountInfo)
        {

            var currentBalance = _accountService.GetCurrentBalance(Convert.ToDouble(accountInfo.FromAccount));

            var currentBalanceAfterTransfer = currentBalance - Convert.ToDouble(accountInfo.TransactionAmount);

            return currentBalanceAfterTransfer;
        }

        public bool HandlesTransactionType(TransactionType transactionType)
        {
            return transactionType == TransactionType.IntraBankTransfer;
        }
    }
}
