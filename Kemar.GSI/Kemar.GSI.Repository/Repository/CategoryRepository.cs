using AutoMapper;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Context;
using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kemar.GSI.Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GsiDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(GsiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var data = await _context.Categories
                .Where(x => x.IsActive)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryResponse>>(data);
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.CategoryId == id && x.IsActive);

            return _mapper.Map<CategoryResponse>(entity);
        }

        public async Task<CategoryResponse?> AddOrUpdateAsync(int? id, CategoryRequest request)
        {
            Category entity;

            if (id.HasValue)
            {
                entity = await _context.Categories
                    .FirstOrDefaultAsync(x => x.CategoryId == id && x.IsActive);

                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    entity.UpdatedAt = DateTime.UtcNow;

                    _context.Categories.Update(entity);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<CategoryResponse>(entity);
                }
            }

            entity = _mapper.Map<Category>(request);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.CategoryId == id && x.IsActive);

            if (entity == null) return false;

            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
