using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Repository.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<CategoryResponse?> AddOrUpdateAsync(int? id, CategoryRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
