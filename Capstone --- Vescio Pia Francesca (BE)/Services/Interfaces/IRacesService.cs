using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IRacesService : CrudService<Race, Race>
    {
        Task<RaceDTO> ReturnRace(int id);

    }
}
