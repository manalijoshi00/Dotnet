using Kemar.GSI.API.Helper.Jwt;
using Kemar.GSI.Model.Request;
using Kemar.GSI.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.GSI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = await _repo.Register(request);

            if (user == null)
                return BadRequest(new { message = "User already exists" });

            return Ok(new
            {
                message = "User registered successfully",
                data = new
                {
                    user.UserId,
                    user.Username,
                    user.Email,
                    user.Role
                }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _repo.Login(request);

           if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = JwtTokenHelper.GenerateToken(
                user.UserId.ToString(),
                user.Username,
                user.Email,
                user.Role,
                _config
            );

            return Ok(new
            {
                message = "Login successful",
                data = new
                {
                    user.UserId,
                    user.Username,
                    user.Email,
                    user.Role,
                   Token = token
                }
            });
        }
   }
}