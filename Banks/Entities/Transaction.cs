using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Banks.Entities
{
    public class Transaction
    {
        public Transaction(Customer customer, BankAccount bankAccountFrom, BankAccount bankAccountTo, double money)
        {
            TransactionId = Guid.NewGuid();
            BankAccountTo = bankAccountTo;
            BankAccountFrom = bankAccountFrom;
            Customer = customer;
            Money = money;
        }

        public Customer Customer { get; }
        public double Money { get; }
        public BankAccount BankAccountFrom { get; }
        public BankAccount BankAccountTo { get; }
        public Guid TransactionId { get; }
    }
}