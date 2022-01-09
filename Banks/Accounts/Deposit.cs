using System;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Deposit : BankAccount
    {
        public Deposit(double balance, double percentage, double transactionLimit)
            : base(balance, percentage, transactionLimit) { }

        public override void BetweenBankAccounts(Customer customer, BankAccount bankAccountFrom, BankAccount bankAccountTo, double money)
        {
            throw new BankException("Operation declined due to it's a Deposit Account");
        }

        public override void CashWithdrawal(Customer customer, BankAccount bankAccount, double money)
        {
            throw new BankException("Operation declined due to it's a Deposit Account");
        }

        public override BankAccount ChangeBalanceAfterTime(int days, BankAccount bankAccount, Bank bank)
        {
            int months = days / 30;
            bankAccount.Balance = bankAccount.Balance / 100 * bank.InterestOnTheBalanceDeposit * months;
            return bankAccount;
        }
    }
}