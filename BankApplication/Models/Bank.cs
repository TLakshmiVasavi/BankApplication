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
    }
}