using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IEventService
    {
        Task<Event> Create(EventModel model);
        Task<Event> Read(int id);
        Task<Event> Update(EventModel model);
        Task<Event> Delete(int id);
        
        Task<EventModel> Get(int id);

        Task<IEnumerable<Event>> GetAll();

        Task<IEnumerable<Event>> GetEventsOfTheDay();
        Task<decimal> ChangeModifier(string name, decimal modifier);
    }
}
