using System.Collections.Generic;
using CTS.Models.Product;

namespace CTS.Repository.Interface
{
    public interface IProductRepository
    {
        List<ProductInventoryModel> GetData();

        bool UpdateProductQuantity(ProductInventoryUpdateModel updateModel);
    }
}
