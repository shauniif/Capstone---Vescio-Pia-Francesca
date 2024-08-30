using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAll();
        Task<IEnumerable<Character>> GetAllByUser(int id);

        Task<Character> Create(CharacterDTO dto);
        Task<Character> Read(int id);
        Task<Character> Update(CharacterDTO dto);
        Task<Character> Delete(int id);
        Task<decimal> CountScore(decimal ecoModifier, int cityid, decimal raceModifier, decimal guildModifier);
        Task<decimal> ChangeScore(int id);

    }

}
