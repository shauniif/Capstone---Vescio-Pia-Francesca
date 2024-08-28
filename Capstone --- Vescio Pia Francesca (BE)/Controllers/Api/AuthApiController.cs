using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authSvc;
        private readonly byte[] _key;

        public AuthApiController(IAuthService authSvc, IConfiguration configuration)
        {
            _authSvc = authSvc;
            _key = System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserViewModel user)
        {
            var newUser = await _authSvc.Create(user);
            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var u = await _authSvc.LoginUser(user);
            if (u != null)
            {
                var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, u.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };
                u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));

                var key = new SymmetricSecurityKey(_key);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                         claims: claims,
                         expires: DateTime.Now.AddMonths(1),
                         signingCredentials: creds
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new LoginResponseModel { Username = u.Username, Token = tokenString});
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

