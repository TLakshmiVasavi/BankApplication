using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BankApplication.Models;

namespace BankApplication
{
    class Reader
    {
        public static ServiceCharges ReadCharges()
        {
            Console.WriteLine("Please enter RTGS and IMPS values");
            int rtgs = Reader.ReadInt(1, 100);
            int imps = Reader.ReadInt(1, 100);
            ServiceCharges charges = new ServiceCharges();
            charges.RTGS = rtgs;
            charges.IMPS = imps;
            return charges;
        }
        public static string ReadMail()
        {
            string mail = Console.ReadLine();
            if (Regex.IsMatch(mail, @"^\w+\@\w+\.[a-zA-z]{2,3}$"))
            {
                return mail;
            }
            else
            {
                Console.WriteLine("Sorry,The mail id is not valid");
                return ReadMail();
            }
        }
        public static string ReadNumber()
        {
            string value = ReadString();
            if (Regex.IsMatch(value, @"^[0-9\.\-]*$"))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Please enter a number");
                return ReadNumber();
            }
        }
        public static string ReadGender()
        {
            string value = ReadString();
            if (Regex.IsMatch(value, @"(?i)((Fe)?male|1|2)"))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Sorry,The Gender is not valid ");
                return ReadGender();
            }
        }
        public static string ReadPassword()
        {
            string pwd = Console.ReadLine();
            if (Regex.IsMatch(pwd, @"^((?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9])).{6,}"))
            {
                return pwd;
            }
            else
            {
                Console.WriteLine("Sorry,The password must contain a digit,letter in uppercase and a special character");
                return ReadPassword();
            }
        }
        public static int ReadInt()
        {
            string val = ReadNumber();
            int value = int.Parse(val);
            if (value < 0)
            {
                Console.WriteLine("Please enter a positive value");
                return ReadInt();
            }
            else
            {
                return value;
            }
        }
        public static int ReadInt(int min, int max)
        {
            string val = ReadNumber();
            int value = int.Parse(val);
            if (value >= min && value <= max)
            {
                return value;
            }
            else
            {
                Console.WriteLine("Please enter a valid value");
                return ReadInt(min, max);
            }
        }

        public static string ReadString()
        {
            string value = Console.ReadLine();
            if (Regex.IsMatch(value, @"^[ ]*$"))
            {
                Console.WriteLine("The value cannot be empty \n Please enter a value ");
                return ReadString();
            }
            else
            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\.\-]*$"))
            {
                Console.WriteLine("Please enter a valid value");
                return ReadString();
            }
            else
            {
                return value;
            }
        }
        public static float ReadFloat()
        {
            float Value = float.Parse(ReadNumber());
            if (Value < 0)
            {
                Console.WriteLine("Please enter a positive value");
                return ReadFloat();
            }
            else
            {
                return Value;
            }
        }
    }
}
