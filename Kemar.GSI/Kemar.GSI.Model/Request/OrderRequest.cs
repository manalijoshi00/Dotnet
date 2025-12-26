namespace Kemar.GSI.Model.Request
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        public List<OrderItemRequest> Products { get; set; } = new();
    }
}
