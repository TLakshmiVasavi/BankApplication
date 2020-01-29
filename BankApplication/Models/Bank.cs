using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Models
{
    class Bank
    {
        public string Name;
        public string Id;
        public List<Staff> Employees;
        public List<Account> Accounts;
        public ServiceCharges Charges;
        public ServiceCharges ChargesToOthers;
        public List<Currency> Currencies;
        public Bank(string name)
        {
            Employees = new List<Staff>();
            Accounts = new List<Account>();
            Currencies = new List<Currency>();
            Name = name;
            Id = Name.Substring(0, 3) + DateTime.Today.ToString("d");
            Charges = new ServiceCharges();
            Charges.AssignCharges(0, 5);
            ChargesToOthers = new ServiceCharges();
            ChargesToOthers.AssignCharges(2, 6);
            Currency currency = new Currency("INR", 1);
            Currencies.Add(currency);
        }
    }
}
