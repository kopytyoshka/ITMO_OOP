using System;

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
        private Guid TransactionId { get; }
    }
}