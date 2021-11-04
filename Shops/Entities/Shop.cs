using System;
using System.Collections.Generic;

namespace Shops.Entities
{
    public class Shop
    {
        public Shop(string name, string address)
        {
            Address = address;
            Name = name;
            Id = Guid.NewGuid();
            ProductsList = new List<Product>();
        }

        public Guid Id { get; }

        public string Name { get; }
        public List<Product> ProductsList { get; }
        private string Address { get; }
    }
}