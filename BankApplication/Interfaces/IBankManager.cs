using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Models;

namespace BankApplication.Interfaces
{
    interface IBankManager
    {
        void CreateAccount(AccountHolder accountHolder,Bank bank);
        bool DeleteAccount(Bank bank, string accountId); 
        Account FindAccount(string accountId, Bank bank);
        AccountHolder FindAccountHolder(string accountHolderId, Bank bank);
        void AssignCharges(Bank bank,ServiceCharges serviceCharges);
        void AssignChargesToOthers(Bank bank,ServiceCharges serviceCharges);
        Bank CreateBank(string bankName);
        void AddCurrency(Bank bank,Currency currency);
        void AddStaff(Bank bank,Staff staff);
        bool RemoveStaff(Bank bank, string employeeId);
        Staff FindEmployee(Bank bank, string empId);
        
    }
}
