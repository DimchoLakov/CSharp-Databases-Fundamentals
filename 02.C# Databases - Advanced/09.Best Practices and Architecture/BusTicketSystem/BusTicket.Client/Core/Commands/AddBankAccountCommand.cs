using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class AddBankAccountCommand : IExecutable
    {
        private const string SuccessfullyCreatedBankAccount = "Successfully created bank account!";
        private const string BankAccountAlreadyExists = "Bank account with account number {0} already exists";

        private readonly IBankAccountService _bankAccountService;

        public AddBankAccountCommand(IBankAccountService bankAccountService)
        {
            this._bankAccountService = bankAccountService;
        }

        public string Execute(string[] args)
        {
            int accountNumber = int.Parse(args[0]);
            decimal initialBalance = decimal.Parse(args[1]);

            var exists = this._bankAccountService.Exists(accountNumber);

            if (exists)
            {
                throw new InvalidOperationException(string.Format(BankAccountAlreadyExists, accountNumber));
            }

            this._bankAccountService.AddBankAccount(accountNumber, initialBalance);

            return SuccessfullyCreatedBankAccount;
        }
    }
}
