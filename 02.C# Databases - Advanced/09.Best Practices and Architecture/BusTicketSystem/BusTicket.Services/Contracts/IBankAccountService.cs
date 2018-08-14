using BusTicket.Models;

namespace BusTicket.Services.Contracts
{
    public interface IBankAccountService
    {
        BankAccount AddBankAccount(int accNumber, decimal balance);

        BankAccount GetBankAccountById(int id);

        bool Exists(int accountNumber);
    }
}
