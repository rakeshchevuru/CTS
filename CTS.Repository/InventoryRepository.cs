using CTS.Models.Adventureworks;
using CTS.Repository.Interface;

namespace CTS.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AwDbContext _db;

        public InventoryRepository(AwDbContext db)
        {
            _db = db;
        }

        public void GetData()
        {

        }
    }
}
