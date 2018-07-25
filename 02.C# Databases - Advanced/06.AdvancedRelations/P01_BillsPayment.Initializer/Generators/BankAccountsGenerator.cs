using System;
using System.Collections.Generic;
using System.Linq;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPayment.Initializer.Generators
{
    public class BankAccountsGenerator
    {
        private Random _rnd;

        public BankAccountsGenerator()
        {
            this._rnd = new Random();
        }

        public HashSet<BankAccount> GenerateBankAccounts(int n)
        {
            var balances = GetBalances(n);
            var bankNames = GetBankNames(n);
            var swiftCodes = GetSwiftCodes(n).ToList();

            var bankAccounts = new HashSet<BankAccount>();

            for (int i = 0; i < n; i++)
            {
                var bankName = bankNames[i];
                var swiftCode = swiftCodes[i];
                var balance = balances[i];

                var bankAccount = new BankAccount(bankName, swiftCode, balance);

                bankAccounts.Add(bankAccount);
            }

            return bankAccounts;
        }

        private List<string> GetSwiftCodes(int n)
        {
            var swiftCodesExample = new string[]
            {
                "2IRJF23ON3FD",
                "O2G3J390GBVF",
                "1F20N23IFMDQ",
                "1J2091DJ12WE",
                "I03JG230GN2V",
                "23INGVWODKSM",
                "N20D9ND192IN",
                "D2N012ND102I",
                "NMCXI00VN0WE",
                "3NG0234BIFMV",
                "QV023NVIO3FQ",
                "3INF20023INF",
                "WIO213N232OI",
                "12IOJ12IRON3",
                "120J09SD0V22",
                "12J9012JN921"
            };

            var swiftCodes = new List<string>();

            for (int i = 0; i < n; i++)
            {
                swiftCodes.Add(swiftCodesExample[this._rnd.Next(0, swiftCodesExample.Length - 1)] + this._rnd.Next(1, 500000));
            }

            return swiftCodes;
        }

        private List<string> GetBankNames(int n)
        {
            var bankNamesExample = new string[]
            {
                "DSK",
                "TSB",
                "SDQ",
                "CBA",
                "HBSC",
                "VSP",
                "PSA",
                "GSS",
                "WSS",
                "KGR",
                "BNM",
                "BNB",
                "FVR"
            };

            var bankNames = new List<string>();

            for (int i = 0; i < n; i++)
            {
                bankNames.Add(bankNamesExample[this._rnd.Next(0, bankNamesExample.Length - 1)] + this._rnd.Next(1, 500000));
            }

            return bankNames;
        }

        private List<decimal> GetBalances(int n)
        {
            var balances = new List<decimal>();
            for (int i = 0; i < n; i++)
            {
                balances.Add(this._rnd.Next(400, 50000));
            }

            return balances;
        }
    }
}
