using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Business.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<ProductResponse?> AddOrUpdateAsync(int? id, ProductRequest request);
        Task<IEnumerable<ProductResponse>> GetByFilterAsync(ProductFilterModel filter);
        Task<bool> DeleteAsync(int id);
    }
}
