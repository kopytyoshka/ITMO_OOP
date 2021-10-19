using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopService _shopService;

        [SetUp]
        public void Setup()
        {
            _shopService = new ShopService();
        }

        [Test]
        public void CheckProductStorageAvailability()
        {
            Product productName = _shopService.AddProductStorage("Молочко: Домик в Селе");
            Assert.AreEqual(productName.Name, "Молочко: Домик в Селе");
        }

        [Test]
        public void AddProductToShopAndShopContainsProduct()
        {
            Shop shop = _shopService.AddShop("У Алисы", "ул. Пушкина 2");
            Product product = _shopService.AddProductStorage("Молочко: Просто Квашино");
            Assert.Contains(_shopService.AddProductShop( shop, product, 10, 100), shop.ProductsList);
        }

        [Test]
        public void ShopExistsAndCheapestShop()
        {
            Shop highPriceShop = _shopService.AddShop("У Володи", "ул. Пушкина 3");
            Shop cheapestShop = _shopService.AddShop("У Володи", "Ул Пушкина 4");
            Product product = _shopService.AddProductStorage("Молочко: Домик в Колхозе");
            _shopService.AddProductShop(highPriceShop, product, 100, 15);
            _shopService.AddProductShop(cheapestShop, product, 10, 14);
            Assert.Contains(_shopService.FindCheapestShop(product), new[] {cheapestShop.Id.ToString()});
        }

        [Test]
        public void NotEnoughMoneyToBuyProduct_ThrowException()
        {
            Shop shop = _shopService.AddShop("У Кости", "ул. Колотушкина 3");
            Product product = _shopService.AddProductStorage("Яблочный сок Огороды придонья");
            _shopService.AddProductShop(shop, product, 100, 100);
            var customer = new Customer("Коля", 250);
            Assert.Catch<ShopException>(() =>
            {
                _shopService.BuyProduct(shop, product, customer, 3);
            });
        }

        [Test]
        public void NotEnoughProductsAtShop_ThrowException()
        {
            Shop shop = _shopService.AddShop("У Алексея", "Кронверкский 49");
            Product product = _shopService.AddProductStorage("Баллы за лабы");
            _shopService.AddProductShop(shop, product, 2, 200);
            var customer = new Customer("Алексей", 2000);
            Assert.Catch<ShopException>(() =>
            {
                _shopService.BuyProduct(shop, product, customer, 3);
            });
        }

        [TestCase(200)]
        [TestCase(222)]
        public void ChangingPricing(int newPrice)
        {
            Shop shop = _shopService.AddShop("У Дианы", "Ломоносова 1");
            Product product = _shopService.AddProductStorage("Пепсикола");
            _shopService.AddProductShop(shop, product, 50, 250);
            _shopService.ChangePrice(shop, product, newPrice);
            Assert.AreEqual(newPrice, shop.ProductsList.Find(product1 => product1.Name == product.Name).Price);
        }
    }
}