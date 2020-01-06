using System.Collections.Generic;
using DemoApp.Services.Models;

namespace DemoApp.Services.AccountService
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccountInfo(int customerId);
        Account GetAccountDetail(int accountNumber);
        List<int> GetAccountNumber(int customerId);
        double GetCurrentBalance(double accountNumber);
    }
}
