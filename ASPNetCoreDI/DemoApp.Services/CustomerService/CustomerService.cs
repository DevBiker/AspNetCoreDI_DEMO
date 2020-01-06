using System;
using System.Diagnostics;
using DemoApp.Services.LoggingService;
using DemoApp.Services.Models;

namespace DemoApp.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {

        private readonly IAccountLogging _accountLogging; 
        public CustomerService(IAccountLogging accountLogging)
        {
            _accountLogging = accountLogging ?? throw new ArgumentException(nameof(accountLogging));
            Debug.WriteLine("Dependency " + this.GetType().Name + " Created");
        }

        public Customer GetCustomer()
        {

            return new Customer { CustomerId =111, CustomerName = "DevBiker" , CustomerAddress = "111 Nowhere Ave", CustomerPhone = "713-876-5309"};
        }
    }
}
