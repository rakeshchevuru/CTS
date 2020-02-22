using System.Collections.Generic;
using System.Threading.Tasks;
using CTS.Api.Client.Interface;
using CTS.Models.Product;
using CTS.Ui.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CTS.Ui.Tests
{
    public class HomeControllerTest
    {
        private readonly Mock<IProductClient> _productClient;

        private readonly HomeController _homeController;

        public HomeControllerTest()
        {
            _productClient = new Mock<IProductClient>();

            _homeController = new HomeController(_productClient.Object);
        }

        [Fact]
        public void Ui_IndexPage_ReturnsViewResult()
        {
            var result = _homeController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Ui_GetProductData_ReturnsJsonProductData()
        {
            _productClient.Setup(x => x.GetProducts()).ReturnsAsync(GetTestData());

            var result = await _homeController.GetProductData();

            var dt = Assert.IsType<JsonResult>(result);
            var data = Assert.IsType<List<ProductInventoryModel>>(dt.Value);
            
            Assert.Equal(2, data.Count);
        }

        [Fact]
        public async Task Ui_UpdateProductInventory_ReturnsOkResult()
        {
            var testData = new ProductInventoryUpdateModel { ProductId = 1, Quantity = 2 };

            _productClient.Setup(x => x.UpdateProductQuantity(testData)).ReturnsAsync(true);

            var result = await _homeController.UpdateProductInventory(testData);

            Assert.IsType<OkResult>(result);
        }

        private List<ProductInventoryModel> GetTestData()
        {
            var data = new List<ProductInventoryModel>
            {
                new ProductInventoryModel
                {
                    ProductId = 1, ProductNumber = "P1", Name = "ProductName1", Category = "C1", SubCategory = "SubC1",
                    Quantity = 1, StandardCost = (decimal) 11.99
                },
                new ProductInventoryModel
                {
                    ProductId = 2, ProductNumber = "P2", Name = "ProductName2", Category = "C2", SubCategory = "SubC2",
                    Quantity = 2, StandardCost = (decimal) 22.99
                }
            };

            return data;
        }
    }
}
