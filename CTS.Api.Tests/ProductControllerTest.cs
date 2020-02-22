using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CTS.Api.Controllers;
using CTS.Api.Helpers;
using CTS.Models.Adventureworks;
using CTS.Models.Product;
using CTS.Repository;
using CTS.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CTS.Api.Tests
{
    public class ProductControllerTest
    {
        private readonly AwDbContext _awDbContext;

        private readonly IProductRepository _prdtRepo;

        private readonly IMapper _mapper;

        private readonly ProductController _productController;

        public ProductControllerTest()
        {
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfileConfiguration()); }).CreateMapper();

            var optionDomainDb = new DbContextOptionsBuilder<AwDbContext>().UseInMemoryDatabase(databaseName: "Aw_InMemory_Database").Options;
            _awDbContext = new AwDbContext(optionDomainDb);

            PrepareData();

            _prdtRepo = new ProductRepository(_awDbContext, _mapper);

            _productController = new ProductController(_prdtRepo);
        }

        [Fact]
        public void Api_GetProductData_ReturnsDataResult()
        {
           var result = _productController.GetProductData();

            var dt = Assert.IsType<ObjectResult>(result);
            var data = Assert.IsType<List<ProductInventoryModel>>(dt.Value);

            Assert.Equal(2, data.Count);
        }

        [Fact]
        public void Api_UpdateProductInventory_ReturnsOkResult()
        {
            var testData = new ProductInventoryUpdateModel {ProductId = 1, Quantity = 2};

            /*Before Update*/
            var data = _prdtRepo.GetData().FirstOrDefault(x => x.ProductId == 1);

            Assert.NotNull(data);
            Assert.Equal(1, data.ProductId);
            Assert.Equal(10, data.Quantity);

            /*Update Call*/
            var result = _productController.UpdateProductInventory(testData);
            
            var dt = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(true, dt.Value);

            /*After Update*/
            data = _prdtRepo.GetData().FirstOrDefault(x => x.ProductId == 1);

            Assert.NotNull(data);
            Assert.Equal(1, data.ProductId);
            Assert.Equal(2, data.Quantity);
        }

        private void PrepareData()
        {
            if (!_awDbContext.Product.Any())
            {
                var catgories = new List<ProductCategory>
                {
                    new ProductCategory {ProductCategoryId = 1, Name = "MyCat1"},
                    new ProductCategory {ProductCategoryId = 2, Name = "MyCat2"}
                };

                var subCategores = new List<ProductSubcategory>
                {
                    new ProductSubcategory {ProductSubcategoryId = 1, ProductCategoryId = 1, Name = "MuSubCat1"},
                    new ProductSubcategory {ProductSubcategoryId = 2, ProductCategoryId = 2, Name = "MuSubCat2"}
                };

                var products = new List<Product>
                {
                    new Product {ProductId = 1, Name = "Product1", Quantity = 10, ProductSubcategoryId = 1},
                    new Product {ProductId = 2, Name = "Product2", Quantity = 20, ProductSubcategoryId = 2}
                };

                _awDbContext.ProductCategory.AddRange(catgories);
                _awDbContext.ProductSubcategory.AddRange(subCategores);
                _awDbContext.Product.AddRange(products);

                _awDbContext.SaveChanges();
            }
        }
    }
}
