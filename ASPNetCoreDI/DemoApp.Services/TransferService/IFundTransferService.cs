using System.Collections.Generic;
using DemoApp.Services.AccountService;
using DemoApp.Services.LoggingService;
using DemoApp.Services.Models;

namespace DemoApp.Services.TransferService
{
  public interface IFundTransferService
    {
      bool SaveWithinCustomerAccountTransaction(IAccountService accountService, IAccountLogging accountLogging, Transaction transaction);

      List<TransactionType> GetFundTransferTypes();

      double GetCurrentBalanceAfterTransfer(IAccountService accountService, Transaction accountInfo);


      bool HandlesTransactionType(TransactionType transactionType);
    }
}
