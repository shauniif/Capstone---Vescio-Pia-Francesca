using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface INationService : CrudService<Nation, NationModel>
    {      
        Task<Nation> GetNation(int id);
        Task<NationModel> Get(int id);

        Task<IEnumerable<Nation>> Search(string query);
    }
}
