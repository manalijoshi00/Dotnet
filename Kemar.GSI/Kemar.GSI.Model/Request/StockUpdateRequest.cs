namespace Kemar.GSI.Model.Request
{
    public class StockUpdateRequest
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime StockDate { get; set; }
    }
}
