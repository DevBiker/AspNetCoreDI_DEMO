using System.Collections.Generic;
using DemoApp.Services.AccountService;
using DemoApp.Services.LoggingService;
using DemoApp.Services.Models;

namespace DemoApp.Services.TransferService
{
  public interface IFundTransferService
    {
      bool SaveWithinCustomerAccountTransaction(Transaction transaction);

      List<TransactionType> GetFundTransferTypes();

      double GetCurrentBalanceAfterTransfer(Transaction accountInfo);


      bool HandlesTransactionType(TransactionType transactionType);
    }
}
