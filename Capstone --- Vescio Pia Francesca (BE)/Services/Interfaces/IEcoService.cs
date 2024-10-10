using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IEcoService : CrudService<Eco, EcoModel>
    {
        Task<Eco> GetEco(int id);
        Task<EcoModel> Get(int id);
        Task<IEnumerable<Eco>> Search(string query);
    }
}
