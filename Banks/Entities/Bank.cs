using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Banks.Entities
{
    public class Bank
    {
        public Bank(double loanInterest, double interestOnTheBalanceDebit, double interestOnTheBalanceDeposit, double transactionLimit, string bankName)
        {
            Customers = new List<Customer>();
            BankAccounts = new List<BankAccount>();
            LoanInterest = loanInterest;
            InterestOnTheBalanceDebit = interestOnTheBalanceDebit;
            InterestOnTheBalanceDeposit = interestOnTheBalanceDeposit;
            BankName = bankName;
            TransactionLimit = transactionLimit;
        }

        public double TransactionLimit { get; set; }
        public string BankName { get; }
        public double LoanInterest { get; set; }
        public double InterestOnTheBalanceDebit { get; set; }
        public double InterestOnTheBalanceDeposit { get; set; }
        public List<Customer> Customers { get; }
        public List<BankAccount> BankAccounts { get; }
    }
}