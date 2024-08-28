﻿using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
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
    public class AuthController : Controller
    {
        private readonly IRoleService _roleSvc;
        private readonly IAuthService _authSvc;

        private readonly byte[] _key;

        public AuthController(IRoleService roleSvc, IAuthService authSvc, IConfiguration configuration)
        {
            _roleSvc = roleSvc;
            _authSvc = authSvc;
            _key = System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        }

        public async Task<IActionResult> AllRoles()
        {
            var roles = await _roleSvc.GetAll();
            return View(roles);
        }

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

        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _roleSvc.Read(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleSvc.Update(role);
                return RedirectToAction("AllRoles", "Auth");
            }
            return View(role);
        }

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
        public async Task<IActionResult> Login(UserViewModel user)
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
                     new Claim(ClaimTypes.Name, u.Name),
                     new Claim(ClaimTypes.Email, u.Email),
                     new Claim(ClaimTypes.SerialNumber, u.AdminCode)
                     };
                u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AllUsers()
            {
                var users = await _authSvc.GetAll();
                return View(users);
            }

            public async Task<IActionResult> DeleteUser(int id)
            {
                await _authSvc.Delete(id);
                return RedirectToAction("AllUsers", "Auth");
            }
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

            public async Task<IActionResult> AddRoleToUser(int userid, string roleName)
            {
                await _authSvc.AddRoleToUser(userid, roleName);
                return RedirectToAction("AllUsers", "Auth");
            }
            public async Task<IActionResult> RemoveRoleToUser(int userid, string roleName)
            {
                await _authSvc.RemoveRoleToUser(userid, roleName);
                return RedirectToAction("AllUsers", "Auth");
            }
        }

    }
