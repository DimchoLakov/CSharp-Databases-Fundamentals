using System;
using System.Collections.Generic;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPayment.Initializer.Generators
{
    public class UsersGenerator
    {
        private Random _rnd;

        public UsersGenerator()
        {
            this._rnd = new Random();
        }

        public HashSet<User> GenerateUsers(int n)
        {
            var firstNames = GetFirstNames(n);
            var lastNames = LastNames(n);
            var emails = GetEmails(n);
            var passwords = GetPasswords(n);

            HashSet<User> users = new HashSet<User>();

            for (int i = 0; i < n; i++)
            {
                User user = new User()
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = emails[i],
                    Password = passwords[i]
                };

                users.Add(user);
            }

            return users;
        }

        private string[] GetPasswords(int n)
        {
            var passwords = new string[n];
            var passwordsLength = passwords.Length;

            for (int i = 0; i < passwordsLength; i++)
            {
                passwords[i] = $"asdfg{this._rnd.Next(0, 1000)}{this._rnd.Next(0, 1000)}{this._rnd.Next(0, 1000)}";
            }

            return passwords;
        }

        private string[] GetEmails(int n)
        {
            var emails = new string[n];
            var emailsLength = emails.Length;
            var domains = new string[]
            {
                "@abv.bg",
                "@yahoo.com",
                "@gmail.com"
            };

            for (int i = 0; i < emailsLength; i++)
            {
                var dm = domains[this._rnd.Next(0, domains.Length)];
                char randomChar = (char)(this._rnd.Next('z'- 'a' + 1) + 'a');
                emails[i] = $"qwerty{this._rnd.Next(0, 1000)}{this._rnd.Next(0, 1000)}{randomChar}{dm}";
            }

            return emails;
        }

        private string[] GetFirstNames(int n)
        {
            var firstNames = new string[]
            {
                "Pesho",
                "Gosho",
                "Ivan",
                "Vlad",
                "Brad",
                "J.J.",
                "M.M.",
                "W.W.",
                "D.D.",
                "E.E.",
                "R.R."
            };

            var firstNamesLength = firstNames.Length;
            var firstNamesArr = new string[n];

            for (int i = 0; i < n; i++)
            {
                firstNamesArr[i] = firstNames[this._rnd.Next(0, firstNamesLength - 1)] + this._rnd.Next(1, 500000);
            }

            return firstNamesArr;
        }

        private string[] LastNames(int n)
        {
            var lastNames = new string[]
            {
                "Peshov",
                "Goshov",
                "Ivanov",
                "Vladimirov",
                "Kirchev",
                "Mirchev",
                "Pirchev",
                "Birchev",
                "Smirchev",
                "Nirchev",
                "Tirchev"
            };

            var lastNamesLength = lastNames.Length;
            var lastNamesArr = new string[n];

            for (int i = 0; i < n; i++)
            {
                lastNamesArr[i] = lastNames[this._rnd.Next(0, lastNamesLength)] + this._rnd.Next(1, 500000);
            }

            return lastNamesArr;
        }
    }
}
