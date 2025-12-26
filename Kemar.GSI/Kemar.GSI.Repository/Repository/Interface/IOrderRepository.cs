using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Repository.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderResponse>> GetAllAsync();
        Task<OrderResponse?> GetByIdAsync(int id);
        Task<OrderResponse?> CreateOrderAsync(OrderRequest request);
    }
}
