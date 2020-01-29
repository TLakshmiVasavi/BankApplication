using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Models;

namespace BankApplication.Interfaces
{
    interface IAccountManager
    {
        float ViewBalance(string accountId,Bank bank);
        bool Deposit(float amount, Currency currency, Account account);
        bool WithDraw(float amount,Account account);
        bool Transfer(string senderAccountId,string receiverAccountId,string senderBankId,string receiverBankId, float amount, Bank senderBank);
    }
}
