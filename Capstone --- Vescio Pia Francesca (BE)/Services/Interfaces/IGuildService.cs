using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IGuildService : CrudService<Guild, GuildModel>
    {
        Task<GuildModel> Get(int id);
        Task<Guild> GetGuild(int id);
        Task<IEnumerable<Guild>> Search(string query);
    }
}
