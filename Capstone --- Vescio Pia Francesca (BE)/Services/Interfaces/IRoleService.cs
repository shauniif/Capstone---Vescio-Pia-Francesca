using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> Create(Role entity);
        Task<Role> Update(Role entity);
        Task<Role> Delete(int id);

        Task<Role> Read(int id);

        Task<Role> RoleAdmin();
        Task<IEnumerable<Role>> GetAll();
    }
}
