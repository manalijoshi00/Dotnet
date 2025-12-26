using AutoMapper;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Context;
using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kemar.GSI.Repository.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly GsiDbContext _context;
        private readonly IMapper _mapper;

        public SupplierRepository(GsiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierResponse>> GetAllAsync()
        {
            var data = await _context.Suppliers
                .Where(x => x.IsActive)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SupplierResponse>>(data);
        }

        public async Task<SupplierResponse?> GetByIdAsync(int id)
        {
            var entity = await _context.Suppliers
                .FirstOrDefaultAsync(x => x.SupplierId == id && x.IsActive);

            return _mapper.Map<SupplierResponse>(entity);
        }

        public async Task<SupplierResponse?> AddOrUpdateAsync(int? id, SupplierRequest request)
        {
            Supplier entity;

            if (id.HasValue)
            {
                entity = await _context.Suppliers
                    .FirstOrDefaultAsync(x => x.SupplierId == id && x.IsActive);

                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    entity.UpdatedAt = DateTime.UtcNow;

                    _context.Suppliers.Update(entity);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<SupplierResponse>(entity);
                }
            }

            entity = _mapper.Map<Supplier>(request);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Suppliers.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<SupplierResponse>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Suppliers
                .FirstOrDefaultAsync(x => x.SupplierId == id && x.IsActive);

            if (entity == null) return false;

            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Suppliers.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
