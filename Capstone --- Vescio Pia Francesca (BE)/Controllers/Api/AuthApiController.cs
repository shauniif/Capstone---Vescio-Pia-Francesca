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
        private readonly string key;
        private readonly string issuer;
        private readonly string audience;
        public AuthApiController(IAuthService authSvc, IConfiguration configuration)
        {
            _authSvc = authSvc;
            key = configuration["Jwt:Key"]!;
            issuer = configuration["Jwt:Issuer"]!;
            audience = configuration["Jwt:Audience"]!;
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

                var k = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
                var signed = new SigningCredentials(k, SecurityAlgorithms.HmacSha256);
                var expiration = DateTime.Now.AddMonths(1);


                var token = new JwtSecurityToken(
                         claims: claims,
                         audience: audience,
                         issuer: issuer,
                         expires: DateTime.Now.AddMonths(1),
                         signingCredentials: signed
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new LoginResponseModel { Username = u.Username, Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

