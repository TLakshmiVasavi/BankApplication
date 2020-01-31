using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Models;

namespace BankApplication.Interfaces
{
    interface IBankManager
    {
        User CreateUser(string name, int age, string gender, string mail, string address, string role);
        void CreateAccount(Bank bank, string name, int age, string gender, string mail, string address);
        bool DeleteAccount(Bank bank, string accountId); 
        Account FindAccount(string accountId, Bank bank);
        AccountHolder FindAccountHolder(string accountHolderId, Bank bank);
        void AssignCharges(Bank bank,ServiceCharges serviceCharges);
        void AssignChargesToOthers(Bank bank,ServiceCharges serviceCharges);
        Bank CreateBank(string bankName);
        void AddCurrency(Bank bank,Currency currency);
        void AddStaff(Bank bank, string name, int age, string gender, string mail, string address,float salary);
        bool RemoveStaff(Bank bank, string employeeId);
        Staff FindEmployee(Bank bank, string empId);
        float GetCharges(Bank bank, float amount, bool isSameBank);
    }
}
