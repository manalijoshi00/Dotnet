using Kemar.GSI.Model.BaseEntity;

namespace Kemar.GSI.Model.Response
{
    public class ProductResponse : CommonEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
    }
}
