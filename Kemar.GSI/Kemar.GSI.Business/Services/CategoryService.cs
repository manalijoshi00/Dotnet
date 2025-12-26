using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Repository.Interface;

namespace Kemar.GSI.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            return await _categoryRepo.GetAllAsync();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            return await _categoryRepo.GetByIdAsync(id);
        }

        public async Task<CategoryResponse?> AddOrUpdateAsync(int? id, CategoryRequest request)
        {
            return await _categoryRepo.AddOrUpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepo.DeleteAsync(id);
        }
    }
}
