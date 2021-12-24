using System;
using Banks.Entities;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BankTests
    {
        private CentralBank _centralBank;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void BadCustomerReachesLimit_ThrowException()
        {
            Bank bank = _centralBank.CreateNewBank(5, 3.5, 5, 10000, "Тинькофф");
            Customer customer = _centralBank.CreateNewCustomer("Ivan", "Ivanov", "Ivanovich");
            BankAccount account = _centralBank.CreateDebit(customer, bank, 100000);
            Assert.Catch<Exception>(() =>
            {
                account.CashWithdrawal(customer, account, 10001);
            });
        }

        [Test]
        public void NotEnoughMoneyForTransaction_ThrowException()
        {
            Bank bank = _centralBank.CreateNewBank(5, 3.5, 5, 10000, "Тинькофф");
            Customer customer = _centralBank.CreateNewCustomer("Ivan", "Ivanov", "Ivanovich");
            customer.AddCustomerInfo(customer, "123456", "Пушкина 2");
            BankAccount account = _centralBank.CreateDebit(customer, bank, 100000);
            account.Replenishment(customer, account, 10000);
            Assert.Catch<Exception>(() =>
            {
                account.CashWithdrawal(customer, account, 110001);
            });
        }

        [Test]
        public void TransactionFromDeposit_ThrowException()
        {
            Bank bank = _centralBank.CreateNewBank(5, 3.5, 5, 10000, "Тинькофф");
            Customer customer = _centralBank.CreateNewCustomer("Ivan", "Ivanov", "Ivanovich");
            customer.AddCustomerInfo(customer, "123456", "Пушкина 2");
            BankAccount account = _centralBank.CreateDeposit(customer, bank, 100000);
            Assert.Catch<Exception>(() =>
            {
                account.CashWithdrawal(customer, account, 1000);
            });
        }

        [Test]
        public void ChangingConditions()
        {
            Bank bank = _centralBank.CreateNewBank(5, 3.5, 5, 10000, "Тинькофф");
            _centralBank.ChangeLoanInterest(bank, 4);
            _centralBank.ChangeInterestOnTheBalanceDebit(bank, 3);
            _centralBank.ChangeInterestOnTheBalanceDeposit(bank, 4);
            _centralBank.ChangeTransactionLimit(bank, 8000);
            Assert.AreEqual(bank.LoanInterest, 4);
            Assert.AreEqual(bank.InterestOnTheBalanceDeposit, 4);
            Assert.AreEqual(bank.InterestOnTheBalanceDebit, 3);
            Assert.AreEqual(bank.TransactionLimit, 8000);
        }

        [Test]
        public void AddedInterestOnTheBalance()
        {
            Bank bank = _centralBank.CreateNewBank(5, 3.5, 5, 10000, "Тинькофф");
            Customer customer = _centralBank.CreateNewCustomer("Ivan", "Ivanov", "Ivanovich");
            customer.AddCustomerInfo(customer, "123456", "Пушкина 2");
            BankAccount account = _centralBank.CreateDebit(customer, bank, 100000);
            _centralBank.TimeMachine(62);
            Assert.AreEqual(account.Balance, 107000);
        }
    }
}