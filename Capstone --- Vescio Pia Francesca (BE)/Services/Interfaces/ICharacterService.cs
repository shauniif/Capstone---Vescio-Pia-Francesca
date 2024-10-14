using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface ICharacterService : CrudService<Character, CharacterDTO>
    {
        Task<IEnumerable<Character>> GetAllByUser(int id);
        Task<decimal> CountScore(decimal ecoModifier, int cityid, decimal raceModifier, decimal guildModifier);
        Task<decimal> ChangeScore(int id);
        Task AddOrRemoveRole(int id, int idRole);
        
    }

}
