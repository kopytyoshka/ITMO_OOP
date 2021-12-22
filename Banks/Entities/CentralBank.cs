using System;
using System.Collections.Generic;
using Banks.Accounts;

namespace Banks.Entities
{
    public class CentralBank
    {
        private List<Transaction> _transactions = new List<Transaction>();
        public CentralBank()
        {
            Banks = new List<Bank>();
        }

        public List<Bank> Banks { get; }

        public Bank CreateNewBank(double loanInterest, double interestOnTheBalanceDebit, double interestOnTheBalanceDeposit, double transactionLimit, string bankName)
        {
            var bank = new Bank(loanInterest, interestOnTheBalanceDebit, interestOnTheBalanceDeposit, transactionLimit, bankName);
            Banks.Add(bank);
            return bank;
        }

        public Customer CreateNewCustomer(string firstName, string lastName, string fatherName)
        {
            return new Customer(firstName, lastName, fatherName);
        }

        public BankAccount CreateDeposit(Customer customer, Bank bank, double balance)
        {
            bank.Customers.Add(customer);
            var deposit = new Deposit(balance, bank.InterestOnTheBalanceDebit, bank.TransactionLimit);
            customer.BankAccounts.Add(deposit);
            bank.BankAccounts.Add(deposit);
            return deposit;
        }

        public Debit CreateDebit(Customer customer, Bank bank, double balance)
        {
            bank.Customers.Add(customer);
            var debit = new Debit(balance, bank.InterestOnTheBalanceDebit, bank.TransactionLimit);
            customer.BankAccounts.Add(debit);
            bank.BankAccounts.Add(debit);
            return debit;
        }

        public Credit CreateCredit(Customer customer, Bank bank, double balance)
        {
            bank.Customers.Add(customer);
            var credit = new Credit(balance, bank.InterestOnTheBalanceDebit, bank.TransactionLimit);
            customer.BankAccounts.Add(credit);
            bank.BankAccounts.Add(credit);
            return credit;
        }

        public void AddCustomerInfo(Customer customer, string passport, string address)
        {
            customer.Passport = passport;
            customer.Address = address;
        }

        public Bank ChangeLoanInterest(Bank bank, double loanInterest)
        {
            bank.LoanInterest = loanInterest;
            return bank;
        }

        public Bank ChangeInterestOnTheBalanceDeposit(Bank bank, double interestOnTheBalance)
        {
            bank.InterestOnTheBalanceDeposit = interestOnTheBalance;
            return bank;
        }

        public Bank ChangeInterestOnTheBalanceDebit(Bank bank, double interestOnTheBalance)
        {
            bank.InterestOnTheBalanceDebit = interestOnTheBalance;
            return bank;
        }

        public Bank ChangeTransactionLimit(Bank bank, double transactionLimit)
        {
            bank.TransactionLimit = transactionLimit;
            return bank;
        }

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

        public void TimeMachine(int days)
        {
            int months = days / 30;
            foreach (Bank bank in Banks)
            {
                foreach (BankAccount bankAccount in bank.BankAccounts)
                {
                    switch (bankAccount)
                    {
                        case Deposit _:
                            bankAccount.Balance = bankAccount.Balance / 100 * bank.InterestOnTheBalanceDeposit * months;
                            break;
                        case Debit _:
                            bankAccount.Balance += bankAccount.Balance / 100 * bank.InterestOnTheBalanceDebit * months;
                            break;
                        case Credit _:
                            bankAccount.Balance += bankAccount.Balance / 100 * bank.LoanInterest * months;
                            break;
                    }
                }
            }
        }
    }
}