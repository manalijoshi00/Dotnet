using AutoMapper;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Model.Response;
using Kemar.GSI.Repository.Context;
using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Kemar.GSI.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GsiDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(GsiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            CreateDefaultAdmin().Wait();
        }

        private async Task CreateDefaultAdmin()
        {
            if (!await _context.Users.AnyAsync(x => x.Role == "Admin"))
            {
                var admin = new User
                {
                    Username = "admin",
                    Email = "admin@gsi.com",
                    Role = "Admin",
                    PasswordHash = ComputeSha256("Admin@123"),
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(admin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<LoginResponse?> Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(x => x.Username == request.Username))
                return null;

            if (request.Role.ToLower() == "admin")
                request.Role = "User";

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Role = request.Role,
                PasswordHash = ComputeSha256(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<LoginResponse>(user);
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            var hash = ComputeSha256(request.Password);

            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Username == request.Username &&
                    x.PasswordHash == hash);

            return user == null ? null : _mapper.Map<LoginResponse>(user);
        }

        private string ComputeSha256(string raw)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
