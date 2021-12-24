using System;
using System.Collections.Generic;
using Banks.Tools;

namespace Banks.Entities
{
    public abstract class BankAccount
    {
        private List<Transaction> _transactions = new List<Transaction>();
        public BankAccount(double balance, double percentage, double transactionLimit)
        {
            AccountId = Guid.NewGuid();
            Balance = balance;
            Percentage = percentage;
            TransactionLimit = transactionLimit;
        }

        public double Balance { get; set; }
        public double TransactionLimit { get; }
        private Guid AccountId { get; }
        private double Percentage { get; }

        public virtual void BetweenBankAccounts(Customer customer, BankAccount bankAccountFrom, BankAccount bankAccountTo, double money)
        {
            if (!customer.CheckFullAccount(customer) && bankAccountFrom.TransactionLimit < money)
                throw new BankException("Fulfill additional info to do this operation");
            if (bankAccountFrom.Balance < money)
                throw new BankException("Not enough Funds");
            if (!customer.CheckFullAccount(customer) && bankAccountFrom.TransactionLimit < money)
                throw new BankException("Fulfill additional info to do this operation");
            var transaction = new Transaction(customer, bankAccountFrom, bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccountFrom.Balance -= money;
            bankAccountTo.Balance += money;
        }

        public virtual void CashWithdrawal(Customer customer, BankAccount bankAccount, double money)
        {
            if (!customer.CheckFullAccount(customer) && bankAccount.TransactionLimit < money)
                throw new BankException("Fulfill additional info to do this operation");
            if (bankAccount.Balance < money)
                throw new BankException("Not enough Funds");
            if (!customer.CheckFullAccount(customer) && bankAccount.TransactionLimit < money)
                throw new BankException("Fulfill additional info to do this operation");
            BankAccount bankAccountTo = null;
            var transaction = new Transaction(customer, bankAccount,  bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccount.Balance -= money;
        }

        public void Replenishment(Customer customer, BankAccount bankAccount, double money)
        {
            BankAccount bankAccountTo = null;
            var transaction = new Transaction(customer, bankAccount,  bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccount.Balance += money;
        }

        public void DiscardTransactionBetweenBankAccounts(Transaction transaction)
        {
            BetweenBankAccounts(transaction.Customer, transaction.BankAccountTo, transaction.BankAccountFrom, transaction.Money);
            _transactions.Remove(transaction);
        }

        public void DiscardTransactionCashWithdrawal(Transaction transaction)
        {
            CashWithdrawal(transaction.Customer, transaction.BankAccountTo, transaction.Money);
            _transactions.Remove(transaction);
        }

        public void DiscardTransactionReplenishment(Transaction transaction)
        {
            Replenishment(transaction.Customer, transaction.BankAccountTo, transaction.Money);
            _transactions.Remove(transaction);
        }
    }
}