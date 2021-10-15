using InGameProject.Business.Abstract;
using InGameProject.Business.Concrete;
using InGameProject.DataAccess.EntityFramework;
using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace InGameProject.WebApiTest
{
    public class ProductApiTest
    {
        private ProductManager _productManager;
        public ProductApiTest()
        {
            _productManager = new ProductManager(new EfProductDal());
        }

        [Fact]
        public void Get_All_Products_Test()
        {
            List<Product> list = new List<Product>()
            {
                new Product()
                {
                ProductId = 1,ProductName = "Dell", CategoryId = 3,
                ProductImageUrl = "https://productimages.hepsiburada.net/s/6/375/9746338971698.jpg",
                ProductDescription = "İthal üründür.", IsActive = true
                },
                new Product()
                {
                ProductId = 1,ProductName = "Acer", CategoryId = 3,
                ProductImageUrl = "https://productimages.hepsiburada.net/s/47/375/10937273778226.jpg",
                ProductDescription = "teşhir ürünü", IsActive = true
                },
                new Product()
                {
                ProductId = 1,ProductName = "Mavi", CategoryId = 2,
                ProductImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXz6rvPOoE5NGpe4t2eleZQVz3kRCv4EDZlg&usqp=CAU",
                ProductDescription = "yerli üretim", IsActive = true
                },
                new Product()
                {
                ProductId = 1,ProductName = "Casper", CategoryId = 3,
                ProductImageUrl = "https://cdn.akakce.com/casper/casper-nirvana-c300-3710-4l05e-n3710-4-gb-500-gb-hd-graphics-5500-15-6-notebook-z.jpg",
                ProductDescription = "çin malı", IsActive = true
                }
            };

            var response = _productManager.GetProductListForUnitTestBL();
            Assert.Equal(list.Count, response.Count);
        }

        [Fact]
        public void Get_Product_By_Mock_Id_Test()
        {
            Product product = new Product()
            {
                ProductId = 1,
                ProductName = "Dell",
                CategoryId = 3,
                ProductImageUrl = "https://productimages.hepsiburada.net/s/6/375/9746338971698.jpg",
                ProductDescription = "İthal üründür.",
                IsActive = true
            };


            var response = _productManager.GetProductForUnitTestByIdBL(product.ProductId);
            Assert.Equal(product.ProductName, response.ProductName);
        }

    }
}
