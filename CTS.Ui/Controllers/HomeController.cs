using System.Diagnostics;
using System.Threading.Tasks;
using CTS.Api.Client.Interface;
using CTS.Models.Product;
using Microsoft.AspNetCore.Mvc;
using CTS.Ui.Models;

namespace CTS.Ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductClient _productClient;

        public HomeController(IProductClient productClient)
        {
            _productClient = productClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [Route("GetProductData")]
        [HttpGet]
        public async Task<IActionResult> GetProductData()
        {
            var data = await _productClient.GetProducts();

            return Json(data);
        }

        [Route("UpdateProductInventory")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductInventory(ProductInventoryUpdateModel updateModel)
        {
            await _productClient.UpdateProductQuantity(updateModel);

            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
