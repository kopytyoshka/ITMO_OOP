using System.Collections.Generic;

namespace Banks.Entities
{
    public class Customer
    {
        public Customer(string firstName, string lastName, string fatherName)
        {
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            Passport = string.Empty;
            Address = string.Empty;
            BankAccounts = new List<BankAccount>();
        }

        public List<BankAccount> BankAccounts { get; }
        private string FirstName { get; }
        private string LastName { get; }
        private string FatherName { get; }
        private string Passport { get; set; }
        private string Address { get; set; }

        public void AddCustomerInfo(Customer customer, string passport, string address)
        {
            customer.Passport = passport;
            customer.Address = address;
        }

        public bool CheckFullAccount(Customer customer)
        {
            bool check = !(customer.Address == string.Empty || customer.Passport == string.Empty);
            return check;
        }
    }
}