using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IAuthService : CrudService<User, UserViewModel>
    {
      
        Task<User> CreateSubAdmin(UserViewModel entity);

        Task<User> Login(LoginAdminModel entity);
        Task<User> LoginUser(LoginModel entity);

        Task<User> AddRoleToUser(int userId, string roleName);
        Task<User> RemoveRoleToUser(int userId, string roleName);
        Task<User> CreateUser(int id);
        Task<User> InsertImage(int id, IFormFile image);
        

    }
}
