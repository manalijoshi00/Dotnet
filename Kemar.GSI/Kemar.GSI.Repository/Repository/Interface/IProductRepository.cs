using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Repository.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse?> GetProductByIdAsync(int id);
        Task<ProductResponse?> AddOrUpdateProductAsync(int? id, ProductRequest request);
        Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(ProductFilterModel filter);
        Task<bool> DeleteProductAsync(int id);
    }
}
