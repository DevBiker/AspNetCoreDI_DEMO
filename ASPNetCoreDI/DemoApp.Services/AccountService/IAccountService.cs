using System.Collections.Generic;
using UnityDemos.Services.Models;

namespace UnityDemos.Services.AccountService
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccountInfo(int customerId);
        Account GetAccountDetail(int accountNumber);
        List<int> GetAccountNumber(int customerId);
        double GetCurrentBalance(double accountNumber);
    }
}
