using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityDemos.Services.AccountService;
using UnityDemos.Services.LoggingService;
using UnityDemos.Services.Models;

namespace UnityDemos.Services.TransferService
{
    public class FundTransferService : IFundTransferService
    {
        private readonly IFundTransferService[] _fundTransferService;



        public FundTransferService(IEnumerable<IFundTransferService> fundTransferService)
        {
            Debug.WriteLine("*** Dependency " + this.GetType().Name + " Created");
            _fundTransferService = fundTransferService.Where(e => e.GetType() != this.GetType()).ToArray();
        }

        public bool SaveWithinCustomerAccountTransaction(IAccountService accountService, IAccountLogging accountLogging, Transaction transaction)
        {
            
            return CreateService(transaction.TypeOfTransaction).SaveWithinCustomerAccountTransaction(accountService, accountLogging, transaction);
        }


        private IFundTransferService CreateService(TransactionType transactionType)
        {
            return _fundTransferService.First(e => e.HandlesTransactionType(transactionType));

        }

        public List<TransactionType> GetFundTransferTypes()
        {
            return Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().ToList();
        }




        public double GetCurrentBalanceAfterTransfer(IAccountService accountService, Transaction transaction)
        {
            var specificService = CreateService(transaction.TypeOfTransaction);
            return specificService.GetCurrentBalanceAfterTransfer(accountService, transaction);

        }

        /// <inheritdoc />
        public bool HandlesTransactionType(TransactionType transactionType)
        {
            return _fundTransferService.Any(e => e.HandlesTransactionType(transactionType)); 
        }
    }
}
