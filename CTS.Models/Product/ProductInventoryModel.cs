namespace CTS.Models.Product
{
    public class ProductInventoryModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public decimal StandardCost { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

    }
}
