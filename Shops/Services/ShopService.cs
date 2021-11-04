using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopService : IShopService
    {
        private List<Product> _productsList = new ();
        private List<Shop> _shopsList = new ();

        public Product AddProductStorage(string name)
        {
            const int inf = int.MaxValue;
            var product = new Product(name, inf);
            _productsList.Add(product);
            return product;
        }

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address);
            _shopsList.Add(shop);
            return shop;
        }

        public Product AddProductToShop(Shop shop, Product prodName, int amount, int price)
        {
            var product = new Product(prodName.Name, amount);
            product.Price = price;
            shop.ProductsList.Add(product);
            return product;
        }

        public void BuyProduct(Shop shopName, Product prodName, Customer person, int amount)
        {
            foreach (Product product in from shop in _shopsList where shop.Id == shopName.Id from product in shop.ProductsList where product.Name == prodName.Name select product)
            {
                if (product.Amount > amount)
                {
                    if (person.Wallet >= amount * product.Price)
                    {
                        person.Wallet -= amount * product.Price;
                        product.Amount -= amount;
                    }
                    else
                    {
                        throw new ShopException("Not enough money");
                    }
                }
                else
                {
                    throw new ShopException("Not enough products at shop");
                }
            }
        }

        public void ChangePrice(Shop shop, Product productName, int newPrice)
        {
            foreach (var product in shop.ProductsList.Where(product => product.Name.Equals(productName.Name)))
            {
                product.Price = newPrice;
            }
        }

        public string FindCheapestShop(Product productName)
        {
            double minCost = double.PositiveInfinity;
            string minShop = null;
            foreach (Shop shop in _shopsList
                .Where(shop => shop.ProductsList.Exists(product => product.Name == productName.Name))
                .Where(shop => shop.ProductsList.Find(product => product.Name == productName.Name).Price < minCost))
            {
                minCost = shop.ProductsList.Find(product => product.Name == productName.Name).Price;
                minShop = shop.Id.ToString();
            }

            return minShop;
        }
    }
}