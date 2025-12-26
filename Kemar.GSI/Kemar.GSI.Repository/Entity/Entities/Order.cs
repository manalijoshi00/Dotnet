using Kemar.GSI.Repository.Entity.BaseEntities;

namespace Kemar.GSI.Repository.Entity.Entities
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }   
        public User User { get; set; } = null!;

        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}