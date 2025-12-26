
namespace Kemar.GSI.Model.Filter
{
    public class ProductFilterModel
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
