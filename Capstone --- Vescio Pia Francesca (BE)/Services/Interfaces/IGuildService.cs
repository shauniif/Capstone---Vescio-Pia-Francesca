using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IGuildService
    {
        Task<Guild> Create(GuildModel entity);
        Task<Guild> Update(GuildModel entity);
        Task<Guild> Delete(int id);
        Task<Guild> Read(int id);
        Task<GuildModel> Get(int id);

        Task<IEnumerable<Guild>> GetAll();
    }
}
