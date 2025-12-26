using AutoMapper;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Context;
using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kemar.GSI.Repository.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly GsiDbContext _context;
        private readonly IMapper _mapper;

        public StockRepository(GsiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockResponse>> GetAllAsync()
        {
            var data = await _context.Stocks
                .Where(x => x.IsActive)
                .Include(x => x.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StockResponse>>(data);
        }

        public async Task<StockResponse?> GetByIdAsync(int id)
        {
            var entity = await _context.Stocks
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.StockId == id && x.IsActive);

            return _mapper.Map<StockResponse>(entity);
        }

        public async Task<StockResponse?> AddStockAsync(StockRequest request)
        {
            var entity = _mapper.Map<Stock>(request);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Stocks.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<StockResponse>(entity);
        }

        public async Task<bool> ReduceStockFIFOAsync(int productId, int quantity)
        {
            var stockList = await _context.Stocks
                .Where(x => x.ProductId == productId && x.IsActive)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            int remaining = quantity;

            foreach (var stock in stockList)
            {
                if (remaining <= 0) break;

                if (stock.Quantity <= remaining)
                {
                    remaining -= stock.Quantity;
                    stock.Quantity = 0;
                    stock.IsActive = false;
                }
                else
                {
                    stock.Quantity -= remaining;
                    remaining = 0;
                }

                stock.UpdatedAt = DateTime.UtcNow;
                _context.Stocks.Update(stock);
            }

            await _context.SaveChangesAsync();

            return remaining == 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Stocks
                .FirstOrDefaultAsync(x => x.StockId == id && x.IsActive);

            if (entity == null) return false;

            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Stocks.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
