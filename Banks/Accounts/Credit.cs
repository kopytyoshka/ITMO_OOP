using Banks.Entities;

namespace Banks.Accounts
{
    public class Credit : BankAccount
    {
        public Credit(double balance, double percentage, double transactionLimit)
            : base(balance, percentage, transactionLimit) { }
    }
}