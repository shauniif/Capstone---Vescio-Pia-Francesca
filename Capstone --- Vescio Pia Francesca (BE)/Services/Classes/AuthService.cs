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

        private string ConvertImage(IFormFile image)
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);
                string urlImg = $"data:image/jpeg;base64,{base64String}";
                return urlImg;
            }

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

            if((role.Name == "Admin" || role.Name == "Sub-Admin") && user.AdminCode == null)
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
                FirstName = entity.FirstName,
                LastName = entity.LastName,
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

            var articles = await _db.Articles.Where(a => a.Author.Id == user.Id).ToListAsync();

            var comments = new List<Comment>();
            foreach (var article in articles) 
            {
                var allComments = await _db.Comments.Where(c => c.Article.Id == article.Id).ToListAsync();
                comments.AddRange(allComments);
                _db.Articles.Remove(article);
            }
            foreach (var comment in comments)
            {
                _db.Comments.Remove(comment);
            }

            var characters = await _db.Characters.Where(c => c.User.Id == id).ToListAsync();
            foreach (var character in characters)
            {
                _db.Characters.Remove(character);
            }

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

        public async Task<User> Login(LoginAdminModel entity)
        {
            var user = await _db.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.AdminCode == entity.AdminCode);
            if (user != null && _passwordEncoder.IsSame(entity.Password, user.Password))
            {
                var userResulted = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    DateBirth = user.DateBirth,
                    AdminCode = user.AdminCode,
                    Email = user.Email,
                    Roles = user.Roles.ToList()
                };
                return userResulted;

            }
            return null!;
        }
        public async Task<User> LoginUser(LoginModel entity)
        {
            var user = await _db.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == entity.Email);
            if (user != null && _passwordEncoder.IsSame(entity.Password, user.Password))
            {
                var userResulted = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    DateBirth = user.DateBirth,
                    Image = user.Image,
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


            if ((role.Name == "Admin" || role.Name == "Sub-Admin") && !user.Roles.Any(r => r.Name == "Admin" || r.Name == "Sub-Admin"))
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
                FirstName = entity.FirstName,
                LastName = entity.LastName,
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

        public async Task<User> CreateUser(int id)
        {   
            var user = await _db.Users.Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                DateBirth = u.DateBirth,
                Username = u.Username,
                Image = u.Image,
                Roles = u.Roles.Select(r => new Role
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToList()
            }).FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                throw new Exception("user not found");
            }
            return user;
        }

        public async Task<User> InsertImage(int id, IFormFile image)
        {
            var user = await GetById(id);
            user.Image = ConvertImage(image);
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }


        public async Task<User> Update(UserViewModel entity)
        {
            var user = await GetById(entity.Id);
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.DateBirth = entity.DateBirth;
            user.Username = entity.Username;
            _db.Update(user);
            await _db.SaveChangesAsync();
                return user;
        }
    }
}
