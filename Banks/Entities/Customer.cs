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

        public string FirstName { get; }
        public string LastName { get; }
        public string FatherName { get; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public List<BankAccount> BankAccounts { get; }
    }
}