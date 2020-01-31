using System;
using System.Collections.Generic;
using BankApplication.Interfaces;
using BankApplication.Models;
using BankApplication.Services;

namespace BankApplication
{
    class Program
    {
        static IBankManager bankManager = new BankManager();
        static IAccountManager accountManager = new AccountManager();

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a bank name");
            string bankName = Reader.ReadString();
            Bank bank=bankManager.CreateBank(bankName);
            int choice;
            do
            {
                Console.WriteLine("Please Enter your role\n" +
                                  "1.Administrator\n" +
                                  "2.Bank staff\n" +
                                  "3.Account Holder\n" +
                                  "4.Exit");
                choice = Reader.ReadInt(1, 4);
                if (choice == 4)
                {
                    break;
                }
                Console.WriteLine("please enter id");
                string id = Reader.ReadString();
                Console.WriteLine("Please enter password");
                string password = Reader.ReadString();
                switch (choice)
                {
                    case 1:
                        if (id == "bank" && password == "bank")
                        {
                            AdminServices(bank);
                        }
                        break;
                    case 2:
                        Staff emp = bankManager.FindEmployee(bank, id);
                        if (emp != null)
                        {
                            if (emp.Password==password)
                            {
                                EmployeeServices(bank);
                            }
                        }
                        else
                        {
                            Console.WriteLine("The ID is not valid");
                        }
                        break;
                    case 3:
                        AccountHolder accountHolder = bankManager.FindAccountHolder(id,bank);
                        if (accountHolder != null)
                        {
                            if (accountHolder.Password ==password)
                            {
                                AccountHolderServices(accountHolder.AccountId, bank);
                            }
                        }
                        else
                        {
                            Console.WriteLine("The Account Holder with given id doesn't exist");
                        }
                        break;
                }
            } while (true);
        }
        static void AdminServices(Bank bank)
        {
            int choice;
            do
            {
                Console.WriteLine("Please enter your choice\n" +
                    "1.Add Staff\n" +
                    "2.Remove Staff\n" +
                    "3.GoBack");
                choice = Reader.ReadInt(1, 3);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please enter the name");
                        string name = Reader.ReadString();
                        Console.WriteLine("Please enter age");
                        int age = Reader.ReadInt(1, 100);
                        Console.WriteLine("Please enter mail ");
                        string mail = Reader.ReadMail();
                        Console.WriteLine("Please enter Address");
                        string address = Reader.ReadString();
                        Console.WriteLine("Please enter the gender 1.Female 2.Male");
                        string gender = Reader.ReadGender();
                        Console.WriteLine("Please enter the salary");
                        float salary = Reader.ReadFloat();
                        bankManager.AddStaff(bank, name, age, gender, mail, address,salary);
                        break;
                    case 2:
                        Console.WriteLine("Please enter the employee id");
                        string id = Console.ReadLine();
                        if (bankManager.RemoveStaff(bank, id))
                        {
                            Console.WriteLine("Employee is removed Successfully");
                        }
                        else
                        {
                            Console.WriteLine("Employee not Found");
                        }
                        break;
                    case 3:
                        break;
                }
            } while (choice != 3);
        }
        static void EmployeeServices(Bank bank)
        {
            int choice;
            string accountId;
            Account account;
            do
            {
                Console.WriteLine("Please enter your choice\n" +
                    " 1.Create Account\n" +
                    " 2.Update Account details\n" +
                    " 3.Delete Account\n" +
                    " 4.Add Currency\n" +
                    " 5.Add Service Charges To Same Bank\n" +
                    " 6.Add Service Charges To Different Bank\n" +
                    " 7.View Account Transactions \n" +
                    " 8.Revert Transaction\n" +
                    " 9.View Reverted Transactions\n" +
                    " 10.GoBack");
                choice = Reader.ReadInt(1, 10);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please enter the name");
                        string name = Reader.ReadString();
                        Console.WriteLine("Please enter age");
                        int age = Reader.ReadInt(1, 100);
                        Console.WriteLine("Please enter mail ");
                        string mail = Reader.ReadMail();
                        Console.WriteLine("Please enter Address");
                        string address = Reader.ReadString();
                        Console.WriteLine("Please enter the gender 1.Female 2.Male");
                        string gender = Reader.ReadGender();
                        bankManager.CreateAccount(bank, name, age, gender, mail, address);
                        break;
                    case 2:
                        Console.WriteLine("Please enter the account id ");
                        accountId = Reader.ReadString();
                        account = bankManager.FindAccount(accountId, bank);
                        User user = new User();
                        do
                        {
                            Console.WriteLine("Please choose the details\n" +
                                "1.Name\n" +
                                "2.Password\n" +
                                "3.Age\n" +
                                "4.mail\n" +
                                "5.Address\n" +
                                "6.Go Back");
                            choice = Reader.ReadInt(1, 6);
                            switch (choice)
                            {
                                case 1:
                                    user.Name=Reader.ReadString();
                                    break;
                                case 2:
                                    user.Password=Reader.ReadPassword();
                                    break;
                                case 3:
                                    user.Age=Reader.ReadInt(1,100);
                                    break;
                                case 4:
                                    user.Mail=Reader.ReadMail();
                                    break;
                                case 5:
                                    user.Address=Reader.ReadString();
                                    break;
                                case 6:
                                    break;
                            }
                        } while (choice != 6);
                        accountManager.UpdateAccount(user,account);
                        break;
                    case 3:
                        Console.WriteLine("Please enter the account id");
                        accountId = Console.ReadLine();
                        bankManager.DeleteAccount(bank, accountId);
                        break;
                    case 4:
                        Console.WriteLine("Please enter the name of the currency");
                        string currencyName = Reader.ReadString();
                        Console.WriteLine("Please enter the exchange rate INR to " + currencyName);
                        float rate = Reader.ReadFloat();
                        Currency currency = new Currency(currencyName, rate);
                        bankManager.AddCurrency(bank, currency);
                        break;
                    case 5:
                        ServiceCharges serviceCharges = Reader.ReadCharges();
                        bankManager.AssignCharges(bank, serviceCharges);
                        break;
                    case 6:
                        ServiceCharges serviceChargesToOthers = Reader.ReadCharges();
                        bankManager.AssignChargesToOthers(bank, serviceChargesToOthers);
                        break;
                    case 7:
                        Console.WriteLine("Please enter the account id ");
                        accountId = Reader.ReadString();
                        account = bankManager.FindAccount(accountId, bank);
                        ViewTransactions(account, false);
                        break;
                    case 8:
                        Console.WriteLine("Revert Transaction ");
                        break;
                    case 9:
                        Console.WriteLine("Please enter the account id ");
                        accountId = Reader.ReadString();
                        account=bankManager.FindAccount(accountId, bank);
                        ViewTransactions(account, true);
                        break;
                    case 10:
                        break;
                }
            } while (choice != 10);
        }
        static void AccountHolderServices(string accountId, Bank bank)
        {
            int choice;
            Account account = bankManager.FindAccount(accountId, bank);
            do
            {
                Console.WriteLine("Please select your choice\n" +
                    "1.Deposit Amount\n" +
                    "2.Withdraw Amount\n" +
                    "3.Transfer Amount\n" +
                    "4.View Transactions\n" +
                    "5.View Balance\n" +
                    "6.Go Back");
                choice = Reader.ReadInt(1, 6);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please select the currency in which you want to deposit the amount");
                        for (int i = 0; i < bank.Currencies.Count; i++)
                        {
                            Console.WriteLine(i + 1 + " " + bank.Currencies[i].Name);
                        }
                        int index = Reader.ReadInt(1, bank.Currencies.Count);
                        Console.WriteLine("Please enter the amount");
                        float amount = Reader.ReadInt();
                        accountManager.Deposit(amount, bank.Currencies[index - 1], account);
                        Console.WriteLine("The Available Balance is" + account.Balance);
                        break;
                    case 2:
                        Console.WriteLine("Please enter the amount");
                        float withdrawAmount = Reader.ReadInt();
                        if (accountManager.Withdraw(withdrawAmount, account))
                        {
                            Console.WriteLine("The Available Balance is" + account.Balance);
                        }
                        else
                        {
                            Console.WriteLine("Sorry,The Balance is not Sufficient");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Please enter the bank id of receiver");
                        string receiverBankId = Reader.ReadString();
                        Console.WriteLine("Please enter the account id of receiver");
                        string receiverAccountId = Reader.ReadString();
                        Console.WriteLine("Please enter the amount to transfer");
                        float transferAmount = Reader.ReadFloat();
                        Account receiverAccount;
                        if (account.bankId == receiverBankId)
                        {
                            if (receiverAccountId == account.Id)
                            {
                                Console.WriteLine("Sorry,You can't transfer money to your account");
                            }
                            else
                            {
                                receiverAccount = bankManager.FindAccount(receiverAccountId, bank);
                                if (receiverAccount == null)
                                {
                                    Console.WriteLine("The account with given id doesn't exist");
                                }
                                else
                                {
                                    float charges=bankManager.GetCharges(bank, transferAmount, true);
                                    accountManager.SendMoney(account, transferAmount, charges, receiverBankId, receiverAccountId);
                                    accountManager.ReceiveMoney(receiverAccount, transferAmount, charges, account.bankId, account.Id);
                                }
                            }
                        }
                        else
                        {
                            float charges = bankManager.GetCharges(bank, transferAmount, false);
                            accountManager.SendMoney(account, transferAmount, charges, receiverBankId, receiverAccountId);
                        }
                        break;
                    case 4:
                        ViewTransactions(account,false);
                        break;
                    case 5:
                        float balance=accountManager.ViewBalance(account);
                        Console.WriteLine("The available balance is " + balance);
                        break;
                    case 6:
                        break;
                }
            } while (choice != 6);
        }
        static void ViewTransactions(Account account,bool isReverted)
        {
            List<Transaction> transactions = accountManager.ViewTransactions(account,isReverted);
            if (transactions.Count == 0)
            {
                Console.WriteLine("No Transactions are occured");
            }
            else
            {
                foreach (Transaction transaction in transactions)
                {
                    Console.WriteLine("A sum of " + transaction.Amount + " is " + transaction.Type + " with transaction id " + transaction.Id);
                }
            }
        }
    }
}