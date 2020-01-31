using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Interfaces;
using BankApplication.Models;
using BankApplication.Enums;

namespace BankApplication.Services
{
    class AccountManager : IAccountManager
    {
        public bool IsBalanceAvailable(Account account, float amount)
        {
            if (account.Balance >= amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public float ViewBalance(Account account)
        {
            return account.Balance; 
        }
        public List<Transaction> ViewTransactions(Account account,bool isReverted)
        {
            return account.Transactions.FindAll(t => t.isReverted == isReverted);
        }
        public bool Withdraw(float amount,Account account)
        {
            if (account.Balance>=amount)
            {
                account.Balance = account.Balance - amount;
                AddTransaction(amount, TransactionType.Withdrawn,account);
                return true;
            }
            return false;
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
        public void UpdateAccount(User user, Account account)
        {
            AccountHolder accountHolder = account.accountHolder;
            if (user.Address != null)
            {
                accountHolder.Address = user.Address;
            }
            if (user.Age != 0)
            {
                accountHolder.Age = user.Age;
            }
            if (user.Mail != null)
            {
                accountHolder.Mail = user.Mail;
            }
            if (user.Name != null)
            {
                accountHolder.Name = user.Name;
            }
            if (user.Password != null)
            {
                accountHolder.Password = user.Password;
            }
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
            account.Transactions.Add(transaction);
        }
        public bool SendMoney(Account account, float amount, float charges, string receiverBankId, string receiverAccountId)
        {
            if (account.Balance >= amount + charges)
            {
                account.Balance = account.Balance - amount - charges;
                AddTransaction(account.bankId, receiverBankId, account.Id, receiverAccountId, amount, TransactionType.Transfered, charges,account);
                return true;
            }
            return false;
        }
        public void ReceiveMoney(Account account, float amount, float charges, string receiverBankId, string receiverAccountId)
        {
            account.Balance = account.Balance + amount;
            AddTransaction(account.bankId, receiverBankId, account.Id, receiverAccountId, amount, TransactionType.Transfered, charges,account);
        }
        public Transaction FindTransaction(Account account, string transactionId)
        {
            return account.Transactions.Find(t => t.Id == transactionId && t.isReverted == false);
        }
        //public Response RevertTransaction(Account account,string transactionId)
        //{
        //    Transaction transaction = FindTransaction(account, transactionId);
        //    Response response = new Response();
        //    if (transaction != null)
        //    {
        //        if (transaction.Type == TransactionType.Deposited)
        //        {
        //            if (IsBalanceAvailable(account, transaction.Amount))
        //            {
        //                account.Balance = account.Balance - transaction.Amount;
        //                transaction.isReverted = true;
        //                response.isRevertSuccessfull = true;
        //                return response;
        //            }
        //            response.isRevertSuccessfull = false;
        //            return response;
        //        }
        //        if (transaction.Type == TransactionType.Withdrawn)
        //        {
        //            account.Balance = account.Balance + transaction.Amount;
        //            transaction.isReverted = true;
        //            response.isRevertSuccessfull = true;
        //            return response; 
        //        }
        //        if (transaction.Type == TransactionType.Transfered)
        //        {
        //            if (account.Id == transaction.ReceiverAccountId)
        //            {
        //                if (IsBalanceAvailable(account, transaction.Amount))
        //                {
        //                    account.Balance = account.Balance - transaction.Amount;
        //                    transaction.isReverted = true;
        //                    response.accountId = transaction.SenderAccountId;
        //                    response.isRevertSuccessfull = true;
        //                    response.isSender = false;
        //                    response.isTransfer = true;
        //                    response.transactionId = transaction.Id;
        //                    return response;
        //                }
        //                response.isRevertSuccessfull =false;
        //                response.isSender = false;
        //                response.isTransfer = true;
        //                return response;
        //            }
        //            else
        //            {
        //            }
        //        }
        //    }
        //}
        
        //public bool RevertSendAmount(Account account, string transactionId)
        //{
        //    Transaction transaction = FindTransaction(account, transactionId);
        //    account.Balance = account.Balance + transaction.Amount + transaction.Charges;
        //    transaction.isReverted = true;
        //    return true;
        //}
    }
}