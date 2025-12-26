
using Kemar.GSI.Model.BaseEntity;

namespace Kemar.GSI.Model.Response
{
    public class StockResponse : CommonEntity
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Quantity { get; set; }
        public DateTime StockDate { get; set; }
    }
}
