namespace Kemar.GSI.Model.Request
{
    public class ProductRequest
    {
        public string Name { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
