using BusTicket.Models;
using BusTicket.Models.Enums;

namespace BusTicket.Services.Contracts
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int customerId);

        void AddCustomer(string firstName, string lastName, Gender gender);

        Customer RemoveCustomer(int customerId);

        void AddBankAccount(int customerId, int bankAccountId);

        void AddHomeTown(int customerId, int townId);

        bool Exists(int customerId);

        void Deposit(decimal amount, int customerId);

        decimal Withdraw(decimal amount, int customerId);

        bool HasBankAccount(int customerId);
    }
}
