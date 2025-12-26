using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Repository.Interface;

namespace Kemar.GSI.Business.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepo;

        public StockService(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IEnumerable<StockResponse>> GetAllAsync()
        {
            return await _stockRepo.GetAllAsync();
        }

        public async Task<StockResponse?> GetByIdAsync(int id)
        {
            return await _stockRepo.GetByIdAsync(id);
        }

        public async Task<StockResponse?> AddStockAsync(StockRequest request)
        {
            return await _stockRepo.AddStockAsync(request);
        }

        public async Task<bool> ReduceStockFIFOAsync(int productId, int quantity)
        {
            return await _stockRepo.ReduceStockFIFOAsync(productId, quantity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _stockRepo.DeleteAsync(id);
        }
    }

}