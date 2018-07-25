using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using P01_BillsPayment.Initializer.Generators;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPayment.Initializer
{
    public class Initilize
    {
        public Initilize()
        {

        }

        public static void Seed(BillsPaymentSystemContext dbContext, int count)
        {
            var users = UsersSeed(dbContext, count);
            var bankAccounts = BankAccountsSeed(dbContext, count);
            var creditCards = CreditCardsSeed(dbContext, count);

            PaymentMethodsSeeds(dbContext, count, users, bankAccounts, creditCards);
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }

        private static void PaymentMethodsSeeds(BillsPaymentSystemContext dbContext, int n, List<User> users, List<BankAccount> bankAccounts, List<CreditCard> creditCards)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                var isCreditCard = rnd.Next() % 2 == 0;

                if (isCreditCard)
                {
                    dbContext.PaymentMethods.Add(new PaymentMethod(users[i], creditCards[i]));
                }
                else
                {
                    dbContext.PaymentMethods.Add(new PaymentMethod(users[i], bankAccounts[i]));
                }
            }

            dbContext.SaveChanges();
        }

        private static List<CreditCard> CreditCardsSeed(BillsPaymentSystemContext dbContext, int n)
        {
            var creditCardGenerator = new CreditCarsGenerator();
            var creditCards = creditCardGenerator.GenerateCreditCards(n);

            foreach (var creditCard in creditCards)
            {
                if (IsValid(creditCard))
                {
                    dbContext.CreditCards.Add(creditCard);
                }
            }

            dbContext.SaveChanges();

            return creditCards.ToList();
        }

        private static List<BankAccount> BankAccountsSeed(BillsPaymentSystemContext dbContext, int n)
        {
            var bankAccountsGenerator = new BankAccountsGenerator();
            var bankAccounts = bankAccountsGenerator.GenerateBankAccounts(n);

            foreach (var bankAccount in bankAccounts)
            {
                if (IsValid(bankAccount))
                {
                    dbContext.BankAccounts.Add(bankAccount);
                }
            }

            dbContext.SaveChanges();

            return bankAccounts.ToList();
        }

        private static List<User> UsersSeed(BillsPaymentSystemContext dbContext, int n)
        {
            var userGenerator = new UsersGenerator();
            var users = userGenerator.GenerateUsers(n);

            foreach (var user in users)
            {
                if (IsValid(user))
                {
                    dbContext.Users.Add(user);
                }
            }

            dbContext.SaveChanges();

            return users.ToList();
        }
    }
}
