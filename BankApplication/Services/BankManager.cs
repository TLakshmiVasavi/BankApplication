using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Interfaces;
using BankApplication.Models;

namespace BankApplication.Services
{
    class BankManager : IBankManager
    {
        public void AddStaff(Bank bank, Staff staff)
        {
            bank.Employees.Add(staff);
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
        public void CreateAccount(AccountHolder accountHolder, Bank bank)
        {
            Account account = new Account();
            account.Balance = 0;
            account.bankId = bank.Id;
            account.Transactions = new List<Transaction>();
            account.Id = accountHolder.Name.Substring(0, 3) + DateTime.Today.ToString("d");
            accountHolder.AccountId = account.Id;
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
            Bank bank = new Bank(bankName);
            return bank;
        }
    }
}
