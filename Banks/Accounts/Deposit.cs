using System;
using Banks.Entities;

namespace Banks.Accounts
{
    public class Deposit : BankAccount
    {
        public Deposit(double balance, double percentage, double transactionLimit)
            : base(balance, percentage, transactionLimit) { }

        public override void BetweenBankAccounts(Customer customer, BankAccount bankAccountFrom, BankAccount bankAccountTo, double money)
        {
            throw new Exception("Operation declined due to it's a Deposit Account");
        }

        public override void CashWithdrawal(Customer customer, BankAccount bankAccount, double money)
        {
            throw new Exception("Operation declined due to it's a Deposit Account");
        }
    }
}