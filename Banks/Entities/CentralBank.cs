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

        public virtual void DiscardTransactionBetweenBankAccounts(Transaction transaction)
        {
            transaction.BankAccountFrom.BetweenBankAccounts(transaction.Customer, transaction.BankAccountTo, transaction.BankAccountFrom, transaction.Money);
            _transactions.Remove(transaction);
        }

        public virtual void DiscardTransactionCashWithdrawal(Transaction transaction)
        {
            transaction.BankAccountFrom.CashWithdrawal(transaction.Customer, transaction.BankAccountTo, transaction.Money);
            _transactions.Remove(transaction);
        }

        public virtual void DiscardTransactionReplenishment(Transaction transaction)
        {
            transaction.BankAccountFrom.Replenishment(transaction.Customer, transaction.BankAccountTo, transaction.Money);
            _transactions.Remove(transaction);
        }

        public void TimeMachine(int days)
        {
            foreach (Bank bank in Banks)
            {
                foreach (BankAccount bankAccount in bank.BankAccounts)
                {
                    bankAccount.ChangeBalanceAfterTime(days, bankAccount, bank);
                }
            }
        }
    }
}