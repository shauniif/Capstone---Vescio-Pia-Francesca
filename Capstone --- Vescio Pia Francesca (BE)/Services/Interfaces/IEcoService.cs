using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IEcoService
    {
        Task<Eco> Create(EcoModel entity);
        Task<Eco> Update(EcoModel entity);
        Task<Eco> Delete(int id);
        Task<Eco> Read(int id);
        Task<EcoModel> Get(int id);
        Task<IEnumerable<Eco>> GetAllEcos();
    }
}
