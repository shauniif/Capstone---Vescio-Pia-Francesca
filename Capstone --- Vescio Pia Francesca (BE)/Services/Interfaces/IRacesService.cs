using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IRacesService
    {
        Task<Race> Create(Race entity);
        Task<Race> Read(int id);
        Task<Race> Update(Race entity);

        Task<Race> Delete(int id);

        Task<IEnumerable<Race>> GetAllRaces();

        Task<RaceDTO> ReturnRace(int id);

    }
}
