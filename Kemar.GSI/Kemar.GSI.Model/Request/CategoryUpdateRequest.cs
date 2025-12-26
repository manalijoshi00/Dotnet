namespace Kemar.GSI.Model.Request
{
    public class CategoryUpdateRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
