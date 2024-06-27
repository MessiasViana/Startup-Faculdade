using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CityWasteManagement.Services;
using CityWasteManagement.Models;
using Microsoft.Extensions.Configuration;
using Startup.Models;

namespace CityWasteManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserViewModel userViewModel)
        {
            var user = _userService.GetByUserName(userViewModel.UserName);

            if (user == null || user.Password != userViewModel.Password)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public IActionResult Register([FromBody] UserViewModel userViewModel)
        {
            var user = new User
            {
                UserName = userViewModel.UserName,
                Password = userViewModel.Password,
                Role = "User" // Default role
            };

            var createdUser = _userService.Add(user);
            return CreatedAtAction(nameof(Authenticate), new { id = createdUser.Id }, createdUser);
        }
    }
}
