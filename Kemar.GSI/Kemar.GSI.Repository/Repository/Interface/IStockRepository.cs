using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Repository.Repository.Interface
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockResponse>> GetAllAsync();
        Task<StockResponse?> GetByIdAsync(int id);
        Task<StockResponse?> AddStockAsync(StockRequest request);
        Task<bool> ReduceStockFIFOAsync(int productId, int quantity);
        Task<bool> DeleteAsync(int id);
    }
}
