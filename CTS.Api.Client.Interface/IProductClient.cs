using CTS.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTS.Api.Client.Interface
{
    public interface IProductClient
    {
        Task<List<ProductInventoryModel>> GetProducts();

        Task<bool> UpdateProductQuantity(ProductInventoryUpdateModel updateModel);
    }
}
