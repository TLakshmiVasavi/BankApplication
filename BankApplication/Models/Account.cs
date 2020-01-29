﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Models
{
    class Account
    {
        public string Id, bankId;
        public float Balance;
        public AccountHolder accountHolder;
        public List<Transaction> Transactions;
    }
}
