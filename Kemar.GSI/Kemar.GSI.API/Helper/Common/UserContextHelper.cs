using Kemar.GSI.Model.BaseEntity;
using System.Security.Claims;

namespace Kemar.GSI.API.Helper.Common
{
    public static class UserContextHelper
    {
        public static void SetUserInformation<T>(ref T sourceEntity, int primaryKey, HttpContext httpContext)
        {
            var userName = httpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";

            if (sourceEntity is not CommonEntity entity)
                return;

            if (primaryKey <= 0)
            {
                entity.CreatedBy = userName;
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedBy = userName;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.IsActive = true;
            }
            else
            {
                entity.UpdatedBy = userName;
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
