using Banks.Entities;

namespace Banks.Accounts
{
    public class Debit : BankAccount
    {
        public Debit(double balance, double percentage, double transactionLimit)
            : base(balance, percentage, transactionLimit) { }
    }
}