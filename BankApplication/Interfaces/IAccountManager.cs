using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Models;
using BankApplication.Enums;

namespace BankApplication.Interfaces
{
    interface IAccountManager
    {
        // Response RevertTransaction(Account account, string transactionId);
        Transaction FindTransaction(Account account, string transactionId);
        bool IsBalanceAvailable(Account account, float amount);
        List<Transaction> ViewTransactions(Account account, bool isReverted);
        float ViewBalance(Account account);
        void Deposit(float amount, Currency currency, Account account);
        bool Withdraw(float amount, Account account);
        bool SendMoney(Account account, float amount, float charges, string receiverBankId, string receiverAccountId);
        void ReceiveMoney(Account account, float amount, float charges, string receiverBankId, string receiverAccountId);
        void AddTransaction(float amount, TransactionType type, Account account);
        void AddTransaction(string senderBankId, string receiverBankId, string senderAccountId, string receiverAccountId, float amount, TransactionType type, float charges,Account account);
        void UpdateAccount(User user,Account account);
    }
}
