
namespace Kemar.GSI.Model.BaseEntity
{
    public class CommonEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        public string? UpdatedBy { get; set; }
        public bool IsActive { get; set; } 
    }
}