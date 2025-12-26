using Kemar.GSI.Repository.Entity.BaseEntities;

namespace Kemar.GSI.Repository.Entity.Entities
{
    public class Stock : BaseEntity
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

        public DateTime StockDate { get; set; } = DateTime.UtcNow;

        public int RemainingQuantity { get; set; }
    }
}
