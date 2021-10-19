using Shops.Entities;

namespace Shops.Services
{
    public interface IShopService
    {
        Product AddProductStorage(string product);
        Shop AddShop(string name, string address);
        Product AddProductShop(Shop shopName, Product productName, int amount, int price);
        void BuyProduct(Shop shopName, Product productName, Customer person, int amount);

        void ChangePrice(Shop shopName, Product productName, int newPrice);
        string FindCheapestShop(Product productName);
    }
}