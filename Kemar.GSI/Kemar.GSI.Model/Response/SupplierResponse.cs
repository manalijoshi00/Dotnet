using Kemar.GSI.Model.BaseEntity;

namespace Kemar.GSI.Model.Response
{
    public class SupplierResponse : CommonEntity
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
