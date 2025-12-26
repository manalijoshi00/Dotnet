using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Exceptions;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Repository.Interface;

namespace Kemar.GSI.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            return await _orderRepo.GetAllAsync();
        }

        public async Task<OrderResponse?> GetByIdAsync(int id)
        {
            return await _orderRepo.GetByIdAsync(id);
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderRequest request)
        {
            var result = await _orderRepo.CreateOrderAsync(request);

            if (result == null)
                throw new BusinessException("Order creation failed");

            return result;
        }

    }

}
