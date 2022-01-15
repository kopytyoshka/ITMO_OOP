using Banks.Entities;

namespace Banks.Accounts
{
    public class Debit : BankAccount
    {
        public Debit(double balance, double percentage, double transactionLimit)
            : base(balance, percentage, transactionLimit) { }
        public override BankAccount ChangeBalanceAfterTime(int days, BankAccount bankAccount, Bank bank)
        {
            int months = days / 30;
            bankAccount.Balance += bankAccount.Balance / 100 * bank.InterestOnTheBalanceDebit * months;
            return bankAccount;
        }
    }
}