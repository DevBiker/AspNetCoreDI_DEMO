using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using DemoApp.Services.AccountService;
using DemoApp.Services.TransferService;
using System;
using DemoApp.Services.Models;

namespace DemoApp.Services.Test
{
    public class Tests
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

            var transferService = new TransferService.OwnAccountTransferService();
            var testTransaction = new Models.Transaction()
            {
                FromAccount = 111,
                IsSelected = true,
                ToAccount = 222,
                TransactionAmount = 100,
                TransactionId = 1,
                TransctionDate = DateTime.Now,
                TypeOfTransaction = TransactionType.OwnAccountTransfer
            };

            //Act 
            var result = transferService.GetCurrentBalanceAfterTransfer(accountService, testTransaction);

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
            var transferService = new OwnAccountTransferService();
            var testTransaction = new DemoApp.Services.Models.Transaction()
            {
                FromAccount = 111,
                IsSelected = true,
                ToAccount = 222,
                TransactionAmount = 600,
                TransactionId = 1,
                TransctionDate = DateTime.Now,
                TypeOfTransaction = TransactionType.OwnAccountTransfer
            };

            //Act 
            var result = transferService.GetCurrentBalanceAfterTransfer(testTransaction);

            //Assert 
            Assert.AreEqual(-100, result, "Balance after transfer is incorrect.");
            Mock.Assert(accountService);
        }
    }
}