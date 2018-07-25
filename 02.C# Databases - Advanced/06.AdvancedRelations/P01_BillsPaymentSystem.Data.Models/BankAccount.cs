using System;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        private const string InsufficientFundsErrorMsg = "Insufficient funds!";
        private const string NegativeAmountErrorMsg = "Amount cannot be negative!";

        public BankAccount()
        {

        }

        public BankAccount(string bankName, string swiftCode, decimal balance) : this()
        {
            this.BankName = bankName;
            this.SwiftCode = swiftCode;
            this.Balance = balance;
        }

        public int BankAccountId { get; set; }

        public decimal Balance { get; private set ; }

        public string BankName { get; set; }

        public string SwiftCode { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount > this.Balance)
            {
                throw new InvalidOperationException(InsufficientFundsErrorMsg);
            }

            if (amount < 0)
            {
                throw new InvalidOperationException(NegativeAmountErrorMsg);
            }

            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException(NegativeAmountErrorMsg);
            }

            this.Balance += amount;
        }
    }
}
