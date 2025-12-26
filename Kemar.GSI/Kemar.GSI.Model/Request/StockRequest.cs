namespace Kemar.GSI.Model.Request
{
    public class StockRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime StockDate { get; set; } = DateTime.UtcNow;
    }
}
