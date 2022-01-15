using System.Collections.Generic;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Credit : BankAccount
    {
        private List<Transaction> _transactions = new List<Transaction>();
        public Credit(double balance, double percentage, double transactionLimit)
            : base(balance, percentage, transactionLimit) { }
        public override void BetweenBankAccounts(Customer customer, BankAccount bankAccountFrom, BankAccount bankAccountTo, double money)
        {
            if (!customer.CheckFullAccount(customer) && bankAccountFrom.TransactionLimit < money)
                throw new BankException("Fulfill additional info to do this operation");
            var transaction = new Transaction(customer, bankAccountFrom, bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccountFrom.Balance -= money;
            bankAccountTo.Balance += money;
        }

        public override void CashWithdrawal(Customer customer, BankAccount bankAccount, double money)
        {
            if (!customer.CheckFullAccount(customer) && bankAccount.TransactionLimit < money)
                throw new BankException("Fulfill additional info to do this operation");
            BankAccount bankAccountTo = null;
            var transaction = new Transaction(customer, bankAccount,  bankAccountTo, money);
            _transactions.Add(transaction);
            bankAccount.Balance -= money;
        }

        public override BankAccount ChangeBalanceAfterTime(int days, BankAccount bankAccount, Bank bank)
        {
            int months = days / 30;
            bankAccount.Balance += bankAccount.Balance / 100 * bank.LoanInterest * months;
            return bankAccount;
        }
    }
}