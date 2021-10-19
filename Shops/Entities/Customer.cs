namespace Shops.Entities
{
    public class Customer
    {
        public Customer(string name, float money)
        {
            Name = name;
            Wallet = money;
        }

        public float Wallet { get; set; }
        private string Name { get; }
    }
}