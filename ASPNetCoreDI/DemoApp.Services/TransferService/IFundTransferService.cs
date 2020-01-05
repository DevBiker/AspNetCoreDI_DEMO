using System.Collections.Generic;
using UnityDemos.Services.AccountService;
using UnityDemos.Services.LoggingService;
using UnityDemos.Services.Models;

namespace UnityDemos.Services.TransferService
{
  public interface IFundTransferService
    {
      bool SaveWithinCustomerAccountTransaction(IAccountService accountService, IAccountLogging accountLogging, Transaction transaction);

      List<TransactionType> GetFundTransferTypes();

      double GetCurrentBalanceAfterTransfer(IAccountService accountService, Transaction accountInfo);


      bool HandlesTransactionType(TransactionType transactionType);
    }
}
