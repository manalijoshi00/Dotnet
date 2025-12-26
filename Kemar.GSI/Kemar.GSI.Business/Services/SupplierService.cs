using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Repository.Interface;
using Kemar.GSI.Business.Interface;

namespace Kemar.GSI.Business.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepo;

        public SupplierService(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public async Task<IEnumerable<SupplierResponse>> GetAllAsync()
        {
            return await _supplierRepo.GetAllAsync();
        }

        public async Task<SupplierResponse?> GetByIdAsync(int id)
        {
            return await _supplierRepo.GetByIdAsync(id);
        }

        public async Task<SupplierResponse?> AddOrUpdateAsync(int? id, SupplierRequest request)
        {
            return await _supplierRepo.AddOrUpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _supplierRepo.DeleteAsync(id);
        }
    }
}
