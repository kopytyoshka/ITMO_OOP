using System;
using System.Collections.Generic;
using System.Transactions;
using Banks.Accounts;

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

        public Guid AccountId { get; }
        public double Balance { get; set; }
        public double Percentage { get; }
        public double TransactionLimit { get; }

        public virtual void BetweenBankAccounts(Customer customer, BankAccount bankAccountFrom, BankAccount bankAccountTo, double money)
        {
            if ((customer.Passport == string.Empty || customer.Address == string.Empty) && bankAccountFrom.TransactionLimit < money)
                throw new Exception("Fulfill additional info to do this operation");
            if (bankAccountFrom is Deposit)
                throw new Exception("You can't do this operation with Deposit");
            if (bankAccountFrom is Debit && bankAccountFrom.Balance < money)
                throw new Exception("Not enough funds");
            var transaction = new Transaction(customer, bankAccountFrom, bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccountFrom.Balance -= money;
            bankAccountTo.Balance += money;
        }

        public virtual void CashWithdrawal(Customer customer, BankAccount bankAccount, double money)
        {
            if ((customer.Passport == string.Empty || customer.Address == string.Empty) && bankAccount.TransactionLimit < money)
                throw new Exception("Fulfill additional info to do this operation");
            if (bankAccount is Deposit)
                throw new Exception("You can't do this operation with Deposit");
            if (bankAccount is Debit && bankAccount.Balance < money)
                throw new Exception("Not enough funds");
            BankAccount bankAccountTo = null;
            var transaction = new Transaction(customer, bankAccount,  bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccount.Balance -= money;
        }

        public virtual void Replenishment(Customer customer, BankAccount bankAccount, double money)
        {
            BankAccount bankAccountTo = null;
            var transaction = new Transaction(customer, bankAccount,  bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccount.Balance += money;
        }

        public virtual void DiscardTransactionBetweenBankAccounts(Transaction transaction)
        {
            BetweenBankAccounts(transaction.Customer, transaction.BankAccountTo, transaction.BankAccountFrom, transaction.Money);
            _transactions.Remove(transaction);
        }

        public virtual void DiscardTransactionCashWithdrawal(Transaction transaction)
        {
            CashWithdrawal(transaction.Customer, transaction.BankAccountTo, transaction.Money);
            _transactions.Remove(transaction);
        }

        public virtual void DiscardTransactionReplenishment(Transaction transaction)
        {
            Replenishment(transaction.Customer, transaction.BankAccountTo, transaction.Money);
            _transactions.Remove(transaction);
        }
    }
}