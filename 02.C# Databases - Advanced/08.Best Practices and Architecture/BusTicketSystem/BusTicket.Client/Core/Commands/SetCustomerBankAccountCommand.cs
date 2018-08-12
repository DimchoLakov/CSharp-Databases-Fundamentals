using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class SetCustomerBankAccountCommand : IExecutable
    {
        private const string CustomerDoesNotExist = "Customer does not exist!";
        private const string SuccessfullySetCustomerBankAccount = "Successfully set customer bank account";

        private readonly ICustomerService _customerService;
        private readonly IBankAccountService _bankAccountService;

        public SetCustomerBankAccountCommand(ICustomerService customerService, IBankAccountService bankAccountService)
        {
            this._customerService = customerService;
            this._bankAccountService = bankAccountService;
        }

        public string Execute(string[] args)
        {
            int customerId = int.Parse(args[0]);
            int bankAccountId = int.Parse(args[1]);

            var customerExists = this._customerService.Exists(customerId);

            if (!customerExists)
            {
                throw new InvalidOperationException(CustomerDoesNotExist);
            }

            this._customerService.AddBankAccount(customerId, bankAccountId);

            return SuccessfullySetCustomerBankAccount;
        }
    }
}
