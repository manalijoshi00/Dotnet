using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Business.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAllAsync();
        Task<OrderResponse?> GetByIdAsync(int id);
        Task<OrderResponse?> CreateOrderAsync(OrderRequest request);
    }
}
