using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Repository.Repository.Interface
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierResponse>> GetAllAsync();
        Task<SupplierResponse?> GetByIdAsync(int id);
        Task<SupplierResponse?> AddOrUpdateAsync(int? id, SupplierRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
