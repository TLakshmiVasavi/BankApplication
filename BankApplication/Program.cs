using System;
using BankApplication.Interfaces;
using BankApplication.Models;
using BankApplication.Services;

namespace BankApplication
{
    class Program
    {
        static IUserManager userManager = new UserManager();
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
                        Console.WriteLine("Please enter the password");
                        string password = Reader.ReadPassword();
                        Console.WriteLine("Please enter age");
                        int age = Reader.ReadInt(1, 100);
                        Console.WriteLine("Please enter mail ");
                        string mail = Reader.ReadMail();
                        Console.WriteLine("Please enter Address");
                        string address = Reader.ReadMail();
                        Console.WriteLine("Please enter the gender 1.Female 2.Male");
                        string gender = Reader.ReadGender();
                        string role = "Staff";
                        Staff employee = (Staff)userManager.CreateUser(name, age, gender, mail, address, role);
                        bankManager.AddStaff(bank, employee);
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
                        Console.WriteLine("Please enter the password");
                        string password = Reader.ReadPassword();
                        Console.WriteLine("Please enter age");
                        int age = Reader.ReadInt(1, 100);
                        Console.WriteLine("Please enter mail ");
                        string mail = Reader.ReadMail();
                        Console.WriteLine("Please enter Address");
                        string address = Reader.ReadMail();
                        Console.WriteLine("Please enter the gender 1.Female 2.Male");
                        string gender = Reader.ReadGender();
                        string role = "AccountHolder";
                        AccountHolder accountHolder = (AccountHolder)userManager.CreateUser(name, age, gender, mail, address, role);
                        bankManager.CreateAccount(accountHolder, bank);
                        break;
                    case 2:
                        //UpdateAccount(bank);
                        break;
                    case 3:
                        Console.WriteLine("Please enter the account id");
                        string accountId = Console.ReadLine();
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
                        accountManager.ViewTransactions(accountId, bank, false);
                        break;
                    case 8:
                        if (accountManager.RevertTransaction(accountId, transactionId, bank))
                        {
                            Console.WriteLine("Transaction Reverted SuccessFully");
                        }
                        else
                        {
                            Console.WriteLine("Sorry,The Transaction can't be reverted");
                        }
                        break;
                    case 9:
                        accountManager.ViewTransactions(accountId, bank, true);
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
                        accountManager.Transfer(account, bank);
                        accountManager.viewBalance();
                        break;
                    case 4:
                        accountManager.showTransactions(false);
                        break;
                    case 5:
                        accountManager.viewBalance();
                        break;
                    case 6:
                        break;
                }
            } while (choice != 6);
        }
    }
}