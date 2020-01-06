using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DemoApp.Services.AccountService;
using DemoApp.Services.LoggingService;
using DemoApp.Services.Models;

namespace DemoApp.Services.TransferService
{
    public class InterBankTransferService : IFundTransferService
    {
        
        public InterBankTransferService()
        {
            Debug.WriteLine("*** Dependency " + this.GetType().Name + " Created");

        }

        public bool SaveWithinCustomerAccountTransaction(IAccountService accountService, IAccountLogging accountLogging, Transaction transaction)
        {
            return true;
        }




        public List<TransactionType> GetFundTransferTypes()
        {
            return Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().ToList();
        }




        public double GetCurrentBalanceAfterTransfer(IAccountService accountService, Transaction accountInfo)
        {

            var currentBalance = accountService.GetCurrentBalance(Convert.ToDouble(accountInfo.FromAccount));

            var currentBalanceAfterTransfer = currentBalance - Convert.ToDouble(accountInfo.TransactionAmount);

            return currentBalanceAfterTransfer;
        }

        /// <inheritdoc />
        public bool HandlesTransactionType(TransactionType transactionType)
        {
            return transactionType == TransactionType.InterBankTransfer;
        }
    }
}
