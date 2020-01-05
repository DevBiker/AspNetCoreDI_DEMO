using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityDemos.Services.LoggingService;
using UnityDemos.Services.Models;

namespace UnityDemos.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private List<Account> AccountList { get; set; }

        private readonly IAccountLogging _accountLogging; 
        public AccountService(IAccountLogging accountLogging)
        {
            _accountLogging = accountLogging ?? throw new ArgumentException(nameof(accountLogging));


            Debug.WriteLine("Dependency " + this.GetType().Name + " Created");
            this.AccountList = new List<Account>{
            new Account(){AccountNumber = 214587, AccountType = "Saving", BranchName="Katy", CurrentBalance=10000, MinBalance=500,DateOfOpening = Convert.ToDateTime("01/08/2008"), CustomerId =111 },
            new Account(){AccountNumber = 325689, AccountType = "Checking", BranchName="Katy", CurrentBalance=40000, MinBalance=500,DateOfOpening = Convert.ToDateTime("01/08/2008"), CustomerId =111 },
            new Account(){AccountNumber = 874512, AccountType = "Kids Savings Account", BranchName="Katy", CurrentBalance=20000, MinBalance=500,DateOfOpening = Convert.ToDateTime("01/08/2008"), CustomerId =111 }
            };
        }

        public IEnumerable<Account> GetAllAccountInfo(int customerId)
        {
            foreach (var account in AccountList)
            {
                _accountLogging.LogAccountAccess(customerId, account.AccountNumber, "Account info retrieved.");
                yield return account; 
            }
            
        }

        public Account GetAccountDetail(int accountNumber)
        {
          var selectedAccountDetail = AccountList.FirstOrDefault(c => c.AccountNumber == accountNumber);
           return selectedAccountDetail;
        }

        public List<int> GetAccountNumber(int customerId)
        {
            var selectedAccoutNumber = AccountList.Select(c => c.AccountNumber).ToList();
            return selectedAccoutNumber;
        }

        public double GetCurrentBalance(double accountNumber)
        {
            var currentBalance = AccountList.Where(x => x.AccountNumber == accountNumber).Select(x => x.CurrentBalance).FirstOrDefault();
            return currentBalance;
        }
    }
}
