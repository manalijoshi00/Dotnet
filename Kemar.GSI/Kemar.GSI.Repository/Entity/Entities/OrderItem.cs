using Kemar.GSI.Repository.Entity.BaseEntities;

namespace Kemar.GSI.Repository.Entity.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }
}
