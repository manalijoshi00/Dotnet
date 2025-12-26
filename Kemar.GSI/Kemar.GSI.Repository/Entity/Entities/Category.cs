using Kemar.GSI.Repository.Entity.BaseEntities;

namespace Kemar.GSI.Repository.Entity.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
