using System;
using System.Linq;
using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Models.Enums;
using BusTicket.Services.Contracts;

namespace BusTicket.Services
{
    public class CustomerService : ICustomerService
    {
        private const string CustomerNotFound = "Customer with id {0} not found!";
        private const string TownNotFound = "Town with id {0} not found!";
        private const string DoesNotHaveBankAccount = "Customer with id {0} does not have bank account!";

        private readonly BusTicketContext _dbContext;
        private readonly IBankAccountService _bankAccountService;
        private readonly ITownService _townService;

        public CustomerService(BusTicketContext dbContext, IBankAccountService bankAccountService, ITownService townService)
        {
            this._dbContext = dbContext;
            this._bankAccountService = bankAccountService;
            this._townService = townService;
        }

        public Customer GetCustomerById(int customerId)
        {
            var customer = this._dbContext
                .Customers
                .FirstOrDefault(x => x.CustomerId == customerId);

            return customer;
        }

        public void AddCustomer(string firstName, string lastName, Gender gender)
        {
            var customer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender
            };

            this._dbContext.Customers.Add(customer);
            this._dbContext.SaveChanges();
        }

        public Customer RemoveCustomer(int customerId)
        {
            var customer = this.GetCustomerById(customerId);

            if (!this.Exists(customerId))
            {
                throw new ArgumentException(string.Format(CustomerNotFound, customerId));
            }

            this._dbContext.Remove(customer);
            this._dbContext.SaveChanges();

            return customer;
        }

        public void AddHomeTown(int customerId, int townId)
        {
            var customer = this.GetCustomerById(customerId);
            var customerExists = this.Exists(customerId);

            if (!customerExists)
            {
                throw new InvalidOperationException(string.Format(CustomerNotFound, customerId));
            }

            var town = this._townService.GetTownById(townId);
            var townExists = this._townService.Exists(townId);

            if (!townExists)
            {
                throw new InvalidOperationException(string.Format(TownNotFound, townId));
            }

            customer.HomeTown = town;

            this._dbContext.SaveChanges();
        }

        public bool Exists(int customerId)
        {
            var customer = this.GetCustomerById(customerId);

            return customer != null;
        }

        public void AddBankAccount(int customerId, int bankAccountId)
        {
            var customer = this.GetCustomerById(customerId);
            var bankAccount = this._bankAccountService.GetBankAccountById(bankAccountId);

            customer.BankAccountId = bankAccountId;
            customer.BankAccount = bankAccount;

            bankAccount.Customer = customer;
            bankAccount.CustomerId = customerId;

            this._dbContext.SaveChanges();
        }

        public void Deposit(decimal amount, int customerId)
        {
            if (!this.Exists(customerId))
            {
                throw new ArgumentException(string.Format(CustomerNotFound, customerId));
            }

            if (!HasBankAccount(customerId))
            {
                throw new ArgumentException(string.Format(DoesNotHaveBankAccount, customerId));
            }

            
        }

        public decimal Withdraw(decimal amount, int customerId)
        {
            throw new System.NotImplementedException();
        }

        public bool HasBankAccount(int customerId)
        {
            var customer = this.GetCustomerById(customerId);

            if (!this.Exists(customerId))
            {
                throw new ArgumentException(string.Format(CustomerNotFound, customerId));
            }

            return customer.BankAccountId != 0;
        }
    }
}
