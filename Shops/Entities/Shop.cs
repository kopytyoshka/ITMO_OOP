using System;
using System.Collections.Generic;

namespace Shops.Entities
{
    public class Shop
    {
        internal Shop(string name, string address)
        {
            Address = address;
            Name = name;
            Id = ShopIdGenerator();
            ProductsList = new List<Product>();
        }

        public Guid Id { get; }

        public string Name { get; }
        public List<Product> ProductsList { get; }
        private string Address { get; }

        private static Guid ShopIdGenerator()
        {
            Guid id = Guid.NewGuid();
            return id;
        }
    }
}