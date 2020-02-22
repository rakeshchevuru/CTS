using CTS.Models.Product;
using CTS.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CTS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult GetProductData()
        {
            var data = _productRepo.GetData();

            return new ObjectResult(data);
        }

        [HttpPost]
        public IActionResult UpdateProductInventory(ProductInventoryUpdateModel updateModel)
        {
            var data = _productRepo.UpdateProductQuantity(updateModel);

            return Ok(data);
        }
    }
}