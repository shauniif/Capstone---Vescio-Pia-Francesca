using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface INationService
    {
        Task<Nation> Create(NationModel entity);
        Task<Nation> Read(int id);
        Task<Nation> GetNation(int id);
        Task<NationModel> Get(int id);
        Task<Nation> Update(NationModel entity);

        Task<Nation> Delete(int id);

        Task<IEnumerable<Nation>> GetAllNations();
    }
}
