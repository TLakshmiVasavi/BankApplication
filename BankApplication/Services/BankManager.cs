using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Interfaces;
using BankApplication.Models;
using BankApplication.Enums;

namespace BankApplication.Services
{
    class BankManager : IBankManager
    {
        
        public User CreateUser(string name, int age, string gender, string mail, string address, string role)
        {
            User user;
            if (role == "AccountHolder")
            {
                user = new AccountHolder();
            }
            else
            {
                user = new Staff();
            }
            user.Name = name;
            user.Age = age;
            user.Gender = Enum.Parse<Gender>(gender);
            user.Mail = mail;
            user.Address = address;
            user.Role = role;
            user.Id = user.Name.Substring(0, 3) + user.Role + DateTime.Today.ToString("d"); ;
            user.Password = user.Name.Substring(0, 3) + user.Role + DateTime.Today.ToString("d");
            return user;
        }
        public void AddStaff(Bank bank, string name, int age, string gender, string mail, string address,float salary)
        {
            Staff employee = (Staff)CreateUser(name, age, gender, mail, address, "Staff");
            employee.salary = salary;
            employee.BankId = bank.Id;
            bank.Employees.Add(employee);
        }
        public bool RemoveStaff(Bank bank, string empId)
        {
            Staff emp=FindEmployee(bank,empId);
            if (emp == null)
            {
                return false;
            }
            else
            {
                bank.Employees.Remove(emp);
                return true;
            }
        }
        public Staff FindEmployee(Bank bank,string empId)
        {
            Staff employee = bank.Employees.Find(e => e.Id == empId);
            return employee;
        }
        public void CreateAccount(Bank bank, string name, int age, string gender, string mail, string address)
        {
            AccountHolder accountHolder = (AccountHolder)CreateUser(name, age, gender, mail, address, "AccountHolder");
            Account account = new Account();
            account.accountHolder = accountHolder;
            account.Balance = 0;
            account.bankId = bank.Id;
            account.Transactions = new List<Transaction>();
            account.Id = accountHolder.Name.Substring(0, 3) + DateTime.Today.ToString("d");
            accountHolder.AccountId = account.Id;
            accountHolder.BankId = bank.Id;
            account.accountHolder = accountHolder;
            bank.Accounts.Add(account);
        }
        public bool DeleteAccount(Bank bank, string accountId )
        {
            Account account = FindAccount(accountId,bank);
            if (account == null)
            {
                return false;
            }
            else
            {
                bank.Accounts.Remove(account);
                return true;
            }
        }
        public Account FindAccount(string accountId, Bank bank)
        {
            return bank.Accounts.Find(a => a.Id == accountId);
        }
        public float GetCharges(Bank bank, float amount,bool isSameBank)
        {
            ServiceCharges serviceCharges;
            if (isSameBank)
            {
                serviceCharges = bank.Charges;
            }
            else
            {
                serviceCharges = bank.ChargesToOthers;
            }
            if (amount < 200000)
            {
                return amount * serviceCharges.IMPS / 100;
            }
            else
            {
                return amount * serviceCharges.RTGS / 100;
            }
        }
        public void AddCurrency(Bank bank, Currency currency)
        {
            bank.Currencies.Add(currency);
        }
        public void AssignCharges(Bank bank, ServiceCharges serviceCharges)
        {
            bank.Charges = serviceCharges;
        }
        public void AssignChargesToOthers(Bank bank, ServiceCharges serviceCharges)
        {
            bank.ChargesToOthers = serviceCharges;
        }
        public AccountHolder FindAccountHolder(string accountHolderId, Bank bank)
        {
            return bank.Accounts.Find(a => a.accountHolder.Id == accountHolderId).accountHolder;
        }

        public Bank CreateBank(string bankName)
        {
            Bank bank = new Bank();
            bank.Employees = new List<Staff>();
            bank.Accounts = new List<Account>();
            bank.Currencies = new List<Currency>();
            bank.Name = bankName;
            bank.Id = bankName.Substring(0, 3) + DateTime.Today.ToString("d");
            bank.Charges = new ServiceCharges();
            bank.Charges.IMPS = 5;
            bank.Charges.RTGS = 0;
            bank.ChargesToOthers = new ServiceCharges();
            bank.ChargesToOthers.IMPS = 6;
            bank.ChargesToOthers.RTGS = 2;
            Currency currency = new Currency("INR", 1);
            bank.Currencies.Add(currency);
            return bank;
        }
    }
}
