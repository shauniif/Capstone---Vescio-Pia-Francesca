using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IRoleService : CrudService<Role, Role>
    {
        Task<Role> RoleAdmin();
    }
}
