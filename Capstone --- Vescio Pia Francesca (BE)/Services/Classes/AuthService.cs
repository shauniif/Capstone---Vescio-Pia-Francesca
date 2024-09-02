using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Password_Crypth_Implementations;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly DataContext _db;

        public AuthService(IPasswordEncoder passwordEncoder, DataContext db)
        {
            _passwordEncoder = passwordEncoder;
            _db = db;
        }

        private string GenerateCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        // trovo l'user e il ruolo, se non è presente tra i suoi ruoli lo aggiunge
        public async Task<User> AddRoleToUser(int userId, string roleName)
        {

            var user = await _db.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
            var role = await _db.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
            }
            if(role.Name == "Admin" || role.Name == "Sub-Admin")
            {
                var adminCode = GenerateCode();
                user.AdminCode = adminCode;
            }
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> Create(UserViewModel entity)
        {
            var defaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            var user = new User
            {
                Name = entity.Name,
                Username = entity.Username,
                Email = entity.Email,
                DateBirth = entity.DateBirth,
                Password = _passwordEncoder.Encode(entity.Password),
            };
            user.Roles.Add(defaultRole);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {
            var user = await GetById(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _db.Users
                    .Include(x => x.Roles)
                    .ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _db.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user!;
        }

        public async Task<User> Login(UserViewModel entity)
        {
            var user = await _db.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.AdminCode == entity.AdminCode);
            if (user != null && _passwordEncoder.IsSame(entity.Password, user.Password))
            {
                var userResulted = new User
                {
                    Name = user.Name,
                    Username = user.Username,
                    DateBirth = user.DateBirth,
                    AdminCode = entity.AdminCode,
                    Email = user.Email,
                    Roles = user.Roles.ToList()
                };
                return userResulted;

            }
            return null!;
        }
        public async Task<User> LoginAdmin(UserViewModel entity)
        {
            var user =  await _db.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == entity.AdminCode);
            if (user != null && _passwordEncoder.IsSame(entity.Password, user.Password))
            {
                var userResulted = new User
                {
                    Name = user.Name,
                    Username = user.Username,
                    DateBirth = user.DateBirth,
                    Email = user.Email,
                    Roles = user.Roles.ToList()
                };
                return userResulted;

            }
            return null!;
        }
        // trovo l'user e il ruolo, se è presente tra i suoi ruoli lo rimuove
        public async Task<User> RemoveRoleToUser(int userId, string roleName)
        {
            var user = await _db.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
            var role = await _db.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
            if (user.Roles.Contains(role))
            {
                user.Roles.Remove(role);
            }

            if (role.Name == "Admin" || role.Name == "Sub-Admin")
            {
                user.AdminCode = null;
            }

            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> CreateSubAdmin(UserViewModel entity)
        {
            var defaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Sub-Admin");
            var user = new User
            {
                Name = entity.Name,
                Username = entity.Username,
                Email = entity.Email,
                DateBirth = entity.DateBirth,
                Password = _passwordEncoder.Encode(entity.Password),
            };
            user.Roles.Add(defaultRole);

            if (defaultRole.Name == "Sub-Admin" || defaultRole.Name == "Admin")
            {
                var adminCode = GenerateCode();
                user.AdminCode = adminCode;

            }
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginUser(LoginModel entity)
        {
            var user = await _db.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == entity.Email);
            if (user != null && _passwordEncoder.IsSame(entity.Password, user.Password))
            {
                var userResulted = new User
                {
                    Name = user.Name,
                    Username = user.Username,
                    DateBirth = user.DateBirth,
                    Email = user.Email,
                    Roles = user.Roles.ToList()
                };
                return userResulted;

            }
            return null!;
        }

        public async Task<User> CreateUser(int id)
        {   
            var user = await _db.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                DateBirth = u.DateBirth,
                Password = u.Password,
                Username = u.Name
            }).FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                throw new Exception("user not found");
            }
            return user;
        }

        
    }
}
