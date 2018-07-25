using System;
using System.Collections.Generic;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPayment.Initializer.Generators
{
    public class CreditCarsGenerator
    {
        private Random _rnd;

        public CreditCarsGenerator()
        {
            this._rnd = new Random();
        }

        public HashSet<CreditCard> GenerateCreditCards(int n)
        {
            var limits = GetLimits(n);
            var cashOwed = GetMoneyOwed(n);
            var expirationDates = GetExpirationDates(n);

            var creditCards = new HashSet<CreditCard>();

            for (int i = 0; i < n; i++)
            {
                var limit = limits[this._rnd.Next(0, limits.Count - 1)];
                var moneyOwed = cashOwed[this._rnd.Next(0, cashOwed.Count - 1)];
                var expirationDate = expirationDates[this._rnd.Next(0, expirationDates.Count - 1)];

                var creditCard = new CreditCard(limit, moneyOwed, expirationDate);

                creditCards.Add(creditCard);
            }

            return creditCards;
        }

        private List<DateTime> GetExpirationDates(int n)
        {
            var expirationDates = new List<DateTime>();

            for (int i = 0; i < n; i++)
            {
                expirationDates.Add(DateTime.Now.AddMonths(this._rnd.Next(-20, 30)));
            }

            return expirationDates;
        }

        private List<decimal> GetMoneyOwed(int n)
        {
            var moneyOwed = new List<decimal>();

            for (int i = 0; i < n; i++)
            {
                moneyOwed.Add(this._rnd.Next(0, 79999));
            }

            return moneyOwed;
        }

        private List<decimal> GetLimits(int n)
        {
            var limits = new List<decimal>();

            for (int i = 0; i < n; i++)
            {
                limits.Add(this._rnd.Next(500, 80000));
            }

            return limits;
        }
    }
}
