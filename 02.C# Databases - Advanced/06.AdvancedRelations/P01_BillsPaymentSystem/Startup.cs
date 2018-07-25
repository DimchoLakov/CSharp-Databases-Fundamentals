using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_BillsPayment.Initializer;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Enums;

namespace P01_BillsPaymentSystem
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new BillsPaymentSystemContext())
            {
                //dbContext.Database.EnsureDeleted();
                //dbContext.Database.EnsureCreated();

                //Console.WriteLine("Database Created!");

                //Initilize.Seed(dbContext, 200);
                //Console.WriteLine("Seed completed!");

                Console.Write("Please enter User ID: ");
                int userId = int.Parse(Console.ReadLine());

                PrintUserDetails(dbContext, userId);

                Console.WriteLine(new string('-', 30));

                Console.Write("Please enter amount to pay bills: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                PayBills(userId, amount, dbContext);
            }
        }

        private static void PrintUserDetails(BillsPaymentSystemContext dbContext, int userId)
        {
            StringBuilder sb = new StringBuilder();

            var user = dbContext
                .Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    FullName = $"{u.FirstName} {u.LastName}",
                    BankAccounts = u
                        .PaymentMethods
                        .Where(p => p.Type == PaymentType.BankAccount)
                        .Select(p => p.BankAccount)
                        .ToList(),
                    CreditCards = u
                        .PaymentMethods
                        .Where(p => p.Type == PaymentType.CreditCard)
                        .Select(p => p.CreditCard)
                        .ToList()
                })
                .FirstOrDefault();

            if (user == null)
            {
                sb.AppendLine($"User with id {userId} not found!");
            }
            else
            {
                sb.AppendLine($"User: {user.FullName}");

                sb.AppendLine($"Bank Accounts:");
                foreach (var bankAccount in user.BankAccounts)
                {
                    sb.AppendLine($"-- ID: {bankAccount.BankAccountId}")
                        .AppendLine($"--- Balance: {bankAccount.Balance:f2}")
                        .AppendLine($"--- Bank: {bankAccount.BankName}")
                        .AppendLine($"--- SWIFT: {bankAccount.SwiftCode}");
                }

                sb.AppendLine($"Credit Cards:");
                foreach (var creditCard in user.CreditCards)
                {
                    sb.AppendLine($"-- ID: {creditCard.CreditCardId}")
                        .AppendLine($"--- Limit: {creditCard.Limit:f2}")
                        .AppendLine($"--- Money Owed: {creditCard.MoneyOwed:f2}")
                        .AppendLine($"--- Limit Left: {creditCard.LimitLeft:f2}")
                        .AppendLine($"--- Expiration Date: {creditCard.ExpirationDate:yyyy/MM}");
                }
            }

            Console.WriteLine(sb.ToString().Trim());
        }

        private static void PayBills(int userId, decimal amount, BillsPaymentSystemContext dbContext)
        {
            var user = dbContext
                .Users
                .Where(u => u.UserId == userId)
                .Select(p => new
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    BankAccounts = p
                        .PaymentMethods
                        .Where(pm => pm.Type == PaymentType.BankAccount)
                        .OrderBy(ba => ba.BankAccountId)
                        .Select(pm => pm.BankAccount)
                        .ToArray(),
                    CreditCards = p
                        .PaymentMethods
                        .Where(pm => pm.Type == PaymentType.CreditCard)
                        .OrderBy(ba => ba.CreditCardId)
                        .Select(pm => pm.CreditCard)
                        .ToArray()
                })
                .FirstOrDefault();

            if (!HasEnoughMoney(amount, user.BankAccounts, user.CreditCards))
            {
                Console.WriteLine($"Not enough money to pay!");
            }

            var amountToPayLeft = PayByBankAccountAsMuchAsPossible(user.BankAccounts, amount, dbContext);

            if (amountToPayLeft > 0)
            {
                PayWithCreditCards(amountToPayLeft, user.CreditCards, dbContext);
            }

            dbContext.SaveChanges();
            Console.WriteLine("All bills have beed paid!");
        }

        private static bool HasEnoughMoney(decimal amount, BankAccount[] bankAccounts, CreditCard[] creditCards)
        {
            return amount < bankAccounts.Sum(x => x.Balance) + creditCards.Sum(x => x.LimitLeft);
        }

        private static decimal PayByBankAccountAsMuchAsPossible(BankAccount[] bankAccounts, decimal amount, BillsPaymentSystemContext context)
        {
            foreach (var account in bankAccounts)
            {
                context.Entry(account).State = EntityState.Unchanged;

                if (account.Balance >= amount)
                {
                    account.Withdraw(amount);
                    amount = 0;
                    break;
                }

                amount -= account.Balance;
                account.Withdraw(account.Balance);
            }

            return amount;
        }

        private static void PayWithCreditCards(decimal amount, CreditCard[] creditCards, BillsPaymentSystemContext context)
        {
            if (creditCards.Select(cc => cc.LimitLeft).Sum() < amount)
            {
                throw new InvalidOperationException("Not enough money in credit card!");
            }

            foreach (var card in creditCards)
            {
                context.Entry(card).State = EntityState.Unchanged;

                if (card.LimitLeft >= amount)
                {
                    card.Withdraw(amount);
                    return;
                }

                amount -= card.LimitLeft;
                card.Withdraw(card.LimitLeft);
            }
        }
    }
}
