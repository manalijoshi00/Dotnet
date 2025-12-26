using AutoMapper;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Model.Exceptions;
using Kemar.GSI.Repository.Context;
using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kemar.GSI.Repository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly GsiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepo;

        public OrderRepository(GsiDbContext context, IMapper mapper, IStockRepository stockRepo)
        {
            _context = context;
            _mapper = mapper;
            _stockRepo = stockRepo;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(x => x.OrderItems)
                .ThenInclude(i => i.Product)
                .Where(x => x.IsActive)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse?> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(x => x.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.OrderId == id && x.IsActive);

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                decimal totalAmount = 0;

                var order = new Order
                {
                    UserId = request.UserId,
                    OrderDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    OrderItems = new List<OrderItem>()
                };

                foreach (var item in request.Products)
                {
                    var success = await _stockRepo.ReduceStockFIFOAsync(
                        item.ProductId,
                        item.Quantity
                    );

                    if (!success)
                        throw new BusinessException(
                            $"Insufficient stock for ProductId {item.ProductId}"
                        );
                    var OrderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    totalAmount += item.Quantity * item.UnitPrice;
                    order.OrderItems.Add(OrderItem);
                }

                order.TotalAmount = totalAmount;

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                var savedOrder = await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .FirstAsync(o => o.OrderId == order.OrderId);

                await transaction.CommitAsync();

                return _mapper.Map<OrderResponse>(savedOrder);
            }
            catch 
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        //public async Task<bool> DeleteOrderAsync(int id)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync();

        //    var order = await _context.Orders
        //        .Include(o => o.OrderItems)
        //        .FirstOrDefaultAsync(o => o.OrderId == id && o.IsActive);

        //    if (order == null)
        //        return false;

        //    foreach (var item in order.OrderItems)
        //    {
        //        await _stockRepo.AddStockAsync(item.ProductId, item.Quantity);
        //    }

        //    order.IsActive = false;
        //    order.UpdatedAt = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //    await transaction.CommitAsync();

        //    return true;
        //}
    }
}

