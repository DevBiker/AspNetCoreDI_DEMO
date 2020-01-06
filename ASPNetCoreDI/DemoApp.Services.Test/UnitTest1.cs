using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using DemoApp.Services.AccountService;
using DemoApp.Services.TransferService;
using System;
using DemoApp.Services.Models;
using DemoApp.Services.LoggingService;
using System.Linq;

namespace DemoApp.Services.Test
{
    
    public class Tests:UnitTestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Returns_Positive_Number_When_Funds_Are_Available()
        {
            //Setup
            var accountService = Mock.Create<IAccountService>();
            accountService.Arrange(a => a.GetCurrentBalance(111)).Returns(500).OccursOnce();
            var accountLoggingSvc = Mock.Create<AccountLoggingService>(); 

            var transferService = new OwnAccountTransferService(accountService, accountLoggingSvc);
            var testTransaction = DeserializeFileResource<Transaction>("sampleTransaction", t => { t.TransctionDate = DateTime.Now; });


            //Act 
            var result = transferService.GetCurrentBalanceAfterTransfer(testTransaction);

            //Assert 
            Assert.AreEqual(400, result, "Balance after transfer is incorrect.");
            Mock.Assert(accountService);
        }

        [Test]
        public void Returns_Negative_Number_When_Funds_Are_NOT_Available()
        {
            //Setup
            var accountService = Mock.Create<IAccountService>();
            accountService.Arrange(a => a.GetCurrentBalance(111)).Returns(500).OccursOnce();
            var accountLoggingService = Mock.Create<IAccountLogging>(); 
            

            var transferService = new OwnAccountTransferService(accountService, accountLoggingService);
            var testTransaction = DeserializeFileResource<Transaction>("sampleTransaction", t => { t.TransctionDate = DateTime.Now; });
            
            //Act 
            var result = transferService.GetCurrentBalanceAfterTransfer( testTransaction);

            //Assert 
            Assert.AreEqual(-100, result, "Balance after transfer is incorrect.");
            Mock.Assert(accountService);
        }

        [Test]
        public void Account_Transfer_Logs_Account_Access()
        {
            //Setup
            var accountService = Mock.Create<IAccountService>();
            var testAccounts = DeserializeFileResource<Account[]>("testAccounts.json");
            accountService.Arrange(a => a.GetAccountDetail(Arg.AnyInt)).Returns((int i) => testAccounts.First(a => a.AccountNumber == i));

            var testTransaction = DeserializeFileResource<Transaction>("sampleTransaction.json", t => { t.TransctionDate = DateTime.Now; });

            var accountLoggingService = Mock.Create<IAccountLogging>();
            accountLoggingService.Arrange(als => als.LogAccountAccess(testTransaction.CustomerId, testTransaction.ToAccount, Arg.AnyString)).OccursOnce();
            accountLoggingService.Arrange(als => als.LogAccountAccess(testTransaction.CustomerId, testTransaction.FromAccount, Arg.AnyString)).OccursOnce();


            var transferService = new OwnAccountTransferService(accountService, accountLoggingService);

            //Act 
            var result = transferService.SaveWithinCustomerAccountTransaction(testTransaction);

            //Assert 
            Mock.Assert(accountLoggingService);
            
            
        }
    }
}