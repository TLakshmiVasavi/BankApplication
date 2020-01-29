using System;

namespace BankApplication.Models
{
    class Transaction
    {
        public string Id;
        public string SenderBankId;
        public string ReceiverBankId;
        public string ReceiverAccountId;
        public string SenderAccountId;
        public float Amount;
        public float Charges;
        public TransactionType Type;
        public bool isReverted;
    }
}
