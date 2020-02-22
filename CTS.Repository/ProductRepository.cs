using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CTS.Models.Adventureworks;
using CTS.Models.Product;
using CTS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CTS.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AwDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(AwDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<ProductInventoryModel> GetData()
        {
            var data = _db.Product
                .Include(x => x.ProductInventory)
                .Include(x => x.ProductSubcategory).ThenInclude(x => x.ProductCategory)
                .ProjectTo<ProductInventoryModel>(_mapper.ConfigurationProvider)
                .ToList();

            return data;
        }

        public bool UpdateProductQuantity(ProductInventoryUpdateModel updateModel)
        {
            var data = _db.Product.Find(updateModel.ProductId);

            data.Quantity = updateModel.Quantity;

            _db.SaveChanges();

            return true;
        }
    }
}
