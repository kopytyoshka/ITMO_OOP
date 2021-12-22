using System;
using Banks.Accounts;
using Banks.Entities;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            CentralBank centralBank = new CentralBank();
            Customer customer1 = new Customer("ivan", "ivanov", "ivanovich");
            Bank tinkoff = centralBank.CreateNewBank(5, 5, 5, 5000, "Tinkoff");
            Bank sber = centralBank.CreateNewBank(4, 4, 4, 4000, "Sber");
            var bankAccountFirst = centralBank.CreateDebit(customer1, tinkoff, 5555);
            string firstName;
            string lastName;
            string fatherName;
            Console.WriteLine("Введите вашу фамилию:");
            firstName = Console.ReadLine();
            Console.WriteLine("Введите ваше имя:");
            lastName = Console.ReadLine();
            Console.WriteLine("Введите ваше отчество:");
            fatherName = Console.ReadLine();
            Customer customer2 = new Customer(firstName, lastName, fatherName);
            Console.WriteLine("Желаете дополнить информацию о себе? Y/N");
            string passport = string.Empty;
            string address = string.Empty;
            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    Console.WriteLine("Введите паспортные данные:");
                    passport = Console.ReadLine();
                    Console.WriteLine("Введите адрес:");
                    address = Console.ReadLine();
                    break;
                case "N":
                    break;
            }

            BankAccount bankAccount = null;
            centralBank.AddCustomerInfo(customer2, passport, address);
            Console.WriteLine("Какой банк выберете?");
            Console.WriteLine("1) Tinkoff");
            Console.WriteLine("2) Sber");
            string bankChoose = Console.ReadLine();
            Console.WriteLine("Выберите, какой счёт вы желаете открыть:");
            Console.WriteLine("1) Дебетовый");
            Console.WriteLine("2) Кредитный");
            Console.WriteLine("3) Депозитный");
            string accountType = Console.ReadLine();
            if (accountType == "1")
            {
                if (bankChoose == "1")
                    bankAccount = centralBank.CreateDebit(customer2, tinkoff, 0);
                if (bankChoose == "2")
                    bankAccount = centralBank.CreateDebit(customer2, sber, 0);
            }

            if (accountType == "2")
            {
                if (bankChoose == "1")
                    bankAccount = centralBank.CreateCredit(customer2, tinkoff, 0);
                if (bankChoose == "2")
                    bankAccount = centralBank.CreateCredit(customer2, sber, 0);
            }

            if (accountType == "3")
            {
                if (bankChoose == "1")
                    bankAccount = centralBank.CreateCredit(customer2, tinkoff, 0);
                if (bankChoose == "2")
                    bankAccount = centralBank.CreateCredit(customer2, sber, 0);
            }

            Console.Write("Текущее состояние вашего счёта: ");
            Console.WriteLine(bankAccount.Balance);
            Console.WriteLine("Невероятным образом Ваш счёт пополнил незнакомец на 5000!");
            centralBank.BetweenBankAccounts(customer1, bankAccountFirst, bankAccount, 5000);
            Console.Write("Текущее состояние вашего счёта: ");
            Console.WriteLine(bankAccount.Balance);
            Console.WriteLine("Прошло 63 дня, на ваш счёт пришли проценты на остаток!");
            centralBank.TimeMachine(63);
            Console.Write("Текущее состояние вашего счёта: ");
            Console.WriteLine(bankAccount.Balance);
            Console.WriteLine("Были изменены условия банка!");
            centralBank.ChangeLoanInterest(tinkoff, 1);
            Console.Write("Процент по кредиту составляет теперь: ");
            Console.WriteLine(tinkoff.LoanInterest);
            Console.Write("Полный функционал данного консольного приложения в разработке...");
        }
    }
}
