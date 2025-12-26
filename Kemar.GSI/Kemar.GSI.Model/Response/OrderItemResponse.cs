using Kemar.GSI.Model.BaseEntity;

namespace Kemar.GSI.Model.Response
{
    public class OrderItemResponse : CommonEntity   
    {
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
  