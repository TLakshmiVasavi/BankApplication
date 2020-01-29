using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Interfaces;
using BankApplication.Models;

namespace BankApplication.Services
{
    class AccountManager : IAccountManager
    {
        public void Withdraw(float amount,Account account)
        {
            if (account.Balance>=amount)
            {
                account.Balance = account.Balance - amount;
                AddTransaction(amount, TransactionType.Withdrawn,account);
            }
        }
        public void Deposit(float amount,Currency currency, Account account)
        {
            float rate = currency.ExchangeRate;
            amount = amount * rate;
            account.Balance = account.Balance + amount;
            AddTransaction(amount, TransactionType.Deposited,account);
        }
        public void AddTransaction(float amount, TransactionType type,Account account)
        {
            Transaction transaction = new Transaction();
            transaction.Id = "TXN" + account.bankId + account.Id + DateTime.Today.ToString("d");
            transaction.Amount = amount;
            transaction.Type = type;
            transaction.isReverted = false;
            account.Transactions.Add(transaction);
        }
        public void AddTransaction(string senderBankId, string receiverBankId, string senderAccountId, string receiverAccountId, float amount, TransactionType type, float charges,Account account)
        {
            Transaction transaction = new Transaction();
            transaction.Id = "TXN" + senderBankId + receiverBankId + senderAccountId + receiverAccountId + DateTime.Today.ToString("d");
            transaction.Amount = amount;
            transaction.Type = type;
            transaction.SenderBankId = senderBankId;
            transaction.SenderAccountId = senderAccountId;
            transaction.ReceiverBankId = receiverBankId;
            transaction.ReceiverAccountId = receiverAccountId;
            transaction.Charges = charges;
            transaction.isReverted = false;
            
        }
        public bool Transfer(string senderBankId,string senderAccountId,string receiverBankId,string receiverAccountId,float amount, Bank bank)
        {
            
            float charges;
            if (senderAccountId == receiverAccountId)
            {
                return false;
            }
            else
            if (bank.Id == receiverBankId)
            {
                Account receiverAccount = bank.FindAccount(receiverAccountId);
                if (receiverAccount == null)
                {
                    return false;
                }
                charges = bank.Charges.getValue(amount);
                if (account.isBalanceAvailable(amount + charges))
                {
                    account.Balance = account.Balance - amount - charges;
                    receiverAccount.Balance = receiverAccount.Balance + amount;
                    Transaction transaction = new Transaction(bank.Id, receiverBankId, account.Id, receiverAccountId, amount, TransactionType.Transfered, charges);
                    account.AddTransaction(transaction);
                    receiverAccount.AddTransaction(transaction);
                }
            }
            else
            {
                charges = bank.ChargesToOthers.getValue(amount);
                if (account.isAvailable(amount + charges))
                {
                    account.Balance = account.Balance - amount - charges;
                    Transaction transaction = new Transaction(bank.Id, receiverBankId, account.Id, receiverAccountId, amount, TransactionType.Transfered, charges);
                    account.AddTransaction(transaction);
                }
            }
        }
    }
}
