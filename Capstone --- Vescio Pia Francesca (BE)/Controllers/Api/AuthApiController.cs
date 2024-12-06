using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel user)
        {
            if (ModelState.IsValid)
            {

            var newUser = await _authSvc.Create(user);
            var userSel = await _authSvc.CreateUser(newUser.Id);
            return Ok(userSel);
            }
            else
            {
                return BadRequest(ModelState.ErrorCount);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var u = await _authSvc.LoginUser(user);
            if (u != null)
            {
                var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, u.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString())
      };
                u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));

                var k = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
                var signed = new SigningCredentials(k, SecurityAlgorithms.HmacSha256);
                var expiration = DateTime.Now.AddDays(1);


                var token = new JwtSecurityToken(
                         claims: claims,
                         audience: audience,
                         issuer: issuer,
                         expires: expiration,
                         signingCredentials: signed
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new LoginResponseModel { User = new UserResponseDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DateBirth = u.DateBirth,
                    Email = u.Email,
                    Username = u.Username,
                    Image = u.Image,
                }, Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPatch("InsertImage")]
        public async Task<IActionResult> InsertImage([FromForm] int id, [FromForm] IFormFile image)
        {
            var user = await _authSvc.InsertImage(id, image);
            var userSel = await _authSvc.CreateUser(user.Id);
            return Ok(userSel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _authSvc.Read(id);
            var userSel = await _authSvc.CreateUser(user.Id);
            return Ok(userSel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserViewModel user)
        {
            if (ModelState.IsValid)
            {   
                var Curruser = await _authSvc.Read(id);
                if (Curruser != null && Curruser.Id == user.Id)
                {
                     await _authSvc.Update(user);
                }

                var loginModel = new LoginModel
                {
                    Email = user.Email,
                    Password = user.Password,
                };
                return Ok(loginModel);
            }
            return BadRequest();
        }
    }
}

