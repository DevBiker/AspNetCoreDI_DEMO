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
        private readonly Dictionary<TransactionType, Type> _transferServices = new Dictionary<TransactionType, Type>();



        public FundTransferService(KeyValuePair<TransactionType, Type>[] transferServices)
        {
            Debug.WriteLine("Dependency " + this.GetType().Name + " Created");
            foreach (var specificServices in transferServices)
            {
                _transferServices[specificServices.Key] = specificServices.Value;
            }
        }

        public bool SaveWithinCustomerAccountTransaction(IAccountService accountService, IAccountLogging accountLogging, Transaction transaction)
        {
            
            return CreateService(transaction.TypeOfTransaction).SaveWithinCustomerAccountTransaction(accountService, accountLogging, transaction);
        }


        private IFundTransferService CreateService(TransactionType transactionType)
        {
            var specificServiceType = _transferServices[transactionType];
            IFundTransferService specificService = Activator.CreateInstance(specificServiceType) as IFundTransferService;
            return specificService; 
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
            return _transferServices.ContainsKey(transactionType); 
        }
    }
}
