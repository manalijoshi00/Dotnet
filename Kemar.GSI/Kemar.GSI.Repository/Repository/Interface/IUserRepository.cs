using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;

namespace Kemar.GSI.Repository.Repository.Interface
{
    public interface IUserRepository
    {
        Task<LoginResponse?> Register(RegisterRequest request);
        Task<LoginResponse?> Login(LoginRequest request);
    }
}
