namespace Kemar.GSI.Model.Request
{
    public class SupplierUpdateRequest
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
