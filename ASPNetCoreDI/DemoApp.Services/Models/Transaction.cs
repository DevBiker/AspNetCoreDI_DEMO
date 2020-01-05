using System;

namespace UnityDemos.Services.Models
{
    public class Transaction
    {
        public int CustomerId { get; set; }
        public int TransactionId { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public TransactionType TypeOfTransaction { get; set; }
        public DateTime TransctionDate { get; set; }
        public double TransactionAmount { get; set; }
        public bool IsSelected { get; set; }

    }

    public enum TransactionType
    {
        OwnAccountTransfer,
        IntraBankTransfer,
        InterBankTransfer
    }
}
