using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Create(UserViewModel entity);
        Task<User> CreateSubAdmin(UserViewModel entity);
        Task<User> Delete(int id);

        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> Login(UserViewModel entity);
        Task<User> LoginUser(LoginModel entity);

        Task<User> AddRoleToUser(int userId, string roleName);
        Task<User> RemoveRoleToUser(int userId, string roleName);
        Task<User> CreateUser(int id);
        Task<User> InsertImage(int id, IFormFile image);
        

    }
}
