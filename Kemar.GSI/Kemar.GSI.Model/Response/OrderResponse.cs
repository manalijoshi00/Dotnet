using Kemar.GSI.Model.BaseEntity;
using Kemar.GSI.Model.Request;

namespace Kemar.GSI.Model.Response
{
    public class OrderResponse : CommonEntity
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public List<OrderItemResponse> Products { get; set; } = new();
        public DateTime OrderDate { get; set; }
    }
}
