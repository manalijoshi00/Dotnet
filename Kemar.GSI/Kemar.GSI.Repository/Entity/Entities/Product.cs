using Kemar.GSI.Repository.Entity.BaseEntities;

namespace Kemar.GSI.Repository.Entity.Entities
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        public Category Category { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
        public ICollection<Stock> StockEntries { get; set; } = new List<Stock>();
    }
}
