using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Business.Interface
{
    public interface IStockService
    {
        Task<IEnumerable<StockResponse>> GetAllAsync();
        Task<StockResponse?> GetByIdAsync(int id);
        Task<StockResponse?> AddStockAsync(StockRequest request);
        Task<bool> ReduceStockFIFOAsync(int productId, int quantity);
        Task<bool> DeleteAsync(int id);
    }
}
