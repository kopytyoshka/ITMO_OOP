namespace Shops.Entities
{
    public class Product
    {
        public Product(string name, int amount)
        {
            Name = name;
            Amount = amount;
            Price = 0;
        }

        public int Price { get; set; }
        public string Name { get; }
        public int Amount { get; set; }
    }
}