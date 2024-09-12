using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Views;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    
    public class AuthController : Controller
    {
        private readonly IRoleService _roleSvc;
        private readonly IAuthService _authSvc;
        private readonly IWebHostEnvironment _env;

        public AuthController(IRoleService roleSvc, IAuthService authSvc, IConfiguration configuration, IWebHostEnvironment env)
        {
            _roleSvc = roleSvc;
            _authSvc = authSvc;
            _env = env;
            
        }
        public async Task<IActionResult> GetUserImage(int id)
        {
            var user = await _authSvc.GetById(id);
            var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/default.jpg");
            Console.WriteLine(defaultImagePath);
            if (user?.Image == null)
            {
                var defaultImageBytes = await System.IO.File.ReadAllBytesAsync(defaultImagePath);
                return File(defaultImageBytes, "image/jpeg");

            }
            var userPhotodata = user.Image.Substring(23);
            byte[] imageBytes = Convert.FromBase64String(userPhotodata);
            return File(imageBytes, "image/jpeg");
        }

        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> AllRoles()
        {
            var roles = await _roleSvc.GetAll();
            return View(roles);
        }


        [Authorize(Policies.IsAdmin)]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleSvc.Create(role);
                return RedirectToAction("AllRoles", "Auth");
            }
            return View(role);
        }
        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _roleSvc.Read(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> EditRole(Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleSvc.Update(role);
                return RedirectToAction("AllRoles", "Auth");
            }
            return View(role);
        }

        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleSvc.Delete(id);
            return RedirectToAction("AllRoles", "Auth");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
            public async Task<IActionResult> Register(UserViewModel userModel)
            {
                if (ModelState.IsValid)
                {
                    var user = await _authSvc.CreateSubAdmin(userModel);
                    return RedirectToAction("DetailNewUser", "Auth", new { id = user.Id });
                }

                return View(userModel);
            }

        [AllowAnonymous]
            public async Task<IActionResult> DetailNewUser(int id)
            {
                var user = await _authSvc.GetById(id);
                return View(user);
            }

        [AllowAnonymous]
            public IActionResult Login()
            {
                return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginAdminModel user)
        {
            if(ModelState.IsValid)
            {
                var u = await _authSvc.Login(user);
                if(u.AdminCode == null)
                    {
                    throw new Exception("Admin not found");
                    }
                  if (u != null)
                    {
                    var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, $"{u.FirstName} {u.LastName}"),
                        new Claim("Id", u.Id.ToString()),
                        new Claim("FirstName", u.FirstName),
                        new Claim("LastName", u.LastName),
                        new Claim(ClaimTypes.NameIdentifier, u.Username),
                        new Claim(ClaimTypes.Email, u.Email),
                        new Claim("BirthDate", u.DateBirth.ToString("dd-MM-yyyy")),
                        new Claim("AdminCode", u.AdminCode)
                        };
                    u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                    }
                    return RedirectToAction("Index", "Home");
                  }
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> AllUsers()
            {
                var users = await _authSvc.GetAll();
                return View(users);
            }
        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> DeleteUser(int id)
            {
                await _authSvc.Delete(id);
                return RedirectToAction("AllUsers", "Auth");
            }
        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> DetailUser(int id)
            {
                // trovo l'user
                var user = await _authSvc.GetById(id);
                // mappo i suoi ruoli
                var userRoles = user.Roles.Select(r => r.Name).ToList();
                // tutti i ruoli
                var allRoles = await _roleSvc.GetAll();
                // mappo i nomi dei ruoli 
                var rolesName = allRoles.Select(role => role.Name).ToList();
                // prendo i ruoli da rimuovere
                var rolesToRemove = rolesName.Where(r => userRoles.Contains(r)).ToList();

                // rimuovo i ruoli
                foreach (var item in rolesToRemove)
                {
                    rolesName.Remove(item);
                }
                ViewBag.Roles = rolesName;
                return View(user);
            }

        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> AddRoleToUser(int userid, string roleName)
            {
                await _authSvc.AddRoleToUser(userid, roleName);
                return RedirectToAction("AllUsers", "Auth");
            }

        [Authorize(Policies.IsAdmin)]
        public async Task<IActionResult> RemoveRoleToUser(int userid, string roleName)
            {
                await _authSvc.RemoveRoleToUser(userid, roleName);
                return RedirectToAction("AllUsers", "Auth");
            }

        public IActionResult Profile()
            {
            return View();   
            }

        public async Task<IActionResult> InsertImage(int id, IFormFile photo)
            {
              var user =  await _authSvc.InsertImage(id, photo);
                return RedirectToAction(nameof(Profile));
            }


        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _authSvc.GetById(id);
            var userModel = new UserViewModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateBirth = user.DateBirth,
                AdminCode = user.AdminCode,
            };

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UserViewModel userModel, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _authSvc.Update(userModel);
                var adminModel = new LoginAdminModel
                {
                    AdminCode = userModel.AdminCode,
                    Password = password
                };
                await Login(adminModel);
                return RedirectToAction("Index", "Home");
            }

            return View(userModel);
        }

        public IActionResult Error401Page()
        {
            return View();
        }
    }


}

