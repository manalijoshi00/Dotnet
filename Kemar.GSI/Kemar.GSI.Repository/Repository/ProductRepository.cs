using AutoMapper;
using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Context;
using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kemar.GSI.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly GsiDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(GsiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id && p.IsActive);

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse?> AddOrUpdateProductAsync(int? id, ProductRequest request)
        {
            Product entity;

            if (id.HasValue)
            {
                entity = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id && p.IsActive);

                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    entity.UpdatedAt = DateTime.UtcNow;

                    _context.Products.Update(entity);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<ProductResponse>(entity);
                }
            }

            entity = _mapper.Map<Product>(request);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(ProductFilterModel filter)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

            if (filter.SupplierId.HasValue)
                query = query.Where(p => p.SupplierId == filter.SupplierId.Value);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            var result = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(result);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == id && x.IsActive);

            if (product == null)
                return false;

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
