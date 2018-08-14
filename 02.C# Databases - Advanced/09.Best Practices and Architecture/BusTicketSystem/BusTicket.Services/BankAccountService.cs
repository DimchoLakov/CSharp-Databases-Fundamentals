using System.Linq;
using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Services.Contracts;

namespace BusTicket.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BusTicketContext _dbContext;

        public BankAccountService(BusTicketContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public BankAccount GetBankAccountById(int id)
        {
            return this._dbContext
                .BankAccounts
                .FirstOrDefault(x => x.BankAccountId == id);
        }

        public BankAccount AddBankAccount(int accNumber, decimal balance)
        {
            var bankAccount = new BankAccount()
            {
                AccountNumber = accNumber,
                Balance = balance
            };

            this._dbContext
                .Add(bankAccount);

            this._dbContext
                .SaveChanges();

            return bankAccount;
        }

        public bool Exists(int accountNumber)
        {
            var bankAccount = this._dbContext
                .BankAccounts
                .FirstOrDefault(x => x.AccountNumber == accountNumber);

            return bankAccount != null;
        }
    }
}
