using Kemar.GSI.Model.BaseEntity;

namespace Kemar.GSI.Model.Response
{
    public class CategoryResponse : CommonEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
