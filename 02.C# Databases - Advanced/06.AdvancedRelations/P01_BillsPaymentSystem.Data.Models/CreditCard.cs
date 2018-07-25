using System;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        private const string CurrentLimitLeftErrorMsg = "Cannot withdraw more money than current limit!";
        private const string NegativeWithdrawErrorMsg = "Amount cannot be negative!";
        private const string AmountTooBigErrorMsg = "Amount cannot exceed your total credit limit!";
        private const string NegativeDepositErrorMgs = "Cannot deposit negative amount!";
        private const string DepositTooBigErrorMsg = "Deposit cannot be more than the amount you owe!";

        public CreditCard()
        {

        }

        public CreditCard(decimal limit, decimal moneyOwed, DateTime expirationDate) : this()
        {
            this.Limit = limit;
            this.MoneyOwed = moneyOwed;
            this.ExpirationDate = expirationDate;
        }

        public int CreditCardId { get; set; }

        public decimal Limit { get; private set; }

        public decimal MoneyOwed { get; private set; }

        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public DateTime ExpirationDate { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException(NegativeWithdrawErrorMsg);
            }

            if (amount > this.MoneyOwed)
            {
                throw new InvalidOperationException(CurrentLimitLeftErrorMsg);
            }

            if (amount > this.Limit)
            {
                throw new InvalidOperationException(AmountTooBigErrorMsg);
            }

            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException(NegativeDepositErrorMgs);
            }

            if (this.MoneyOwed < amount)
            {
                throw new InvalidOperationException(DepositTooBigErrorMsg);
            }

            this.MoneyOwed -= amount;
        }
    }
}
