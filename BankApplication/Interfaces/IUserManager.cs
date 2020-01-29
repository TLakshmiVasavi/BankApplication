using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Models;

namespace BankApplication.Interfaces
{
    interface IUserManager
    {
        User CreateUser(string name, int age, string gender, string mail, string address,string role);
        
    }
}
