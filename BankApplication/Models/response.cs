using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Models
{
    class Response
    {
        public bool isTransfer;
        public bool isSender;
        public string transactionId;
        public string accountId;
        public bool isRevertSuccessfull;
    }
}
