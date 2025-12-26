namespace Kemar.GSI.Model.Request
{
    public class OrderUpdateRequest
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new();
    }
}
