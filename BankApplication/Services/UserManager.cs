using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Interfaces;
using BankApplication.Models;

namespace BankApplication.Services
{
    class UserManager : IUserManager
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
            return user;
        }

        
    }
}
