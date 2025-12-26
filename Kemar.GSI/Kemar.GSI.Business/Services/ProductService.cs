using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Repository.Interface;

namespace Kemar.GSI.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            return await _productRepo.GetAllProductsAsync();
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            return await _productRepo.GetProductByIdAsync(id);
        }

        public async Task<ProductResponse?> AddOrUpdateAsync(int? id, ProductRequest request)
        {
            return await _productRepo.AddOrUpdateProductAsync(id, request);
        }

        public async Task<IEnumerable<ProductResponse>> GetByFilterAsync(ProductFilterModel filter)
        {
            return await _productRepo.GetProductsByFilterAsync(filter);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepo.DeleteProductAsync(id);
        }
    }

}