using Kemar.GSI.Repository.Entity.BaseEntities;

namespace Kemar.GSI.Repository.Entity.Entities
{
    public class Supplier : BaseEntity
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
