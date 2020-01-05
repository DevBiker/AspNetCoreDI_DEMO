using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityDemos.Services.AccountService;
using UnityDemos.Services.LoggingService;
using UnityDemos.Services.Models;

namespace UnityDemos.Services.TransferService
{
    public class OwnAccountTransferService : IFundTransferService
    {
        public OwnAccountTransferService()
        {
            Debug.WriteLine("*** Dependency " + this.GetType().Name + " Created");
        }

        public bool SaveWithinCustomerAccountTransaction( IAccountService accountService, IAccountLogging accountLogging, Transaction transaction)
        {
            //Get both accounts. 
            var fromAccount = accountService.GetAccountDetail(transaction.FromAccount);
            accountLogging.LogAccountAccess(transaction.CustomerId, fromAccount.AccountNumber, "FROM Account Transfer");
            var toAccount = accountService.GetAccountDetail(transaction.ToAccount);
            accountLogging.LogAccountAccess(transaction.CustomerId, toAccount.AccountNumber, "TO Account Transfer");
            if (fromAccount.CustomerId != toAccount.CustomerId)
            {
                return false;
            }
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

        public bool HandlesTransactionType(TransactionType transactionType)
        {
            return transactionType == TransactionType.OwnAccountTransfer;
        }
    }
}
