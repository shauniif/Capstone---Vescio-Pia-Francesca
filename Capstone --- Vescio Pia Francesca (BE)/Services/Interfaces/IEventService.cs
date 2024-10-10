using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IEventService : CrudService<Event, EventModel>
    {
        Task<EventModel> Get(int id);
        Task<IEnumerable<Event>> GetEventsOfTheDay();
        Task<decimal> ChangeModifier(string name, decimal entityModifier, Event currEvent);
        Task CalcuateModifier(IEnumerable<Eco> ecos, IEnumerable<Guild> guilds, IEnumerable<Nation> nations, IEnumerable<Race> races, IEnumerable<Character> characters);
    }
}
