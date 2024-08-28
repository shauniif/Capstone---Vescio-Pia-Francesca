using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventModel> Create(EventModel model);
        Task<IEnumerable<Event>> Read(string name);
        Task<EventModel> Update(EventModel model);
        Task<IEnumerable<Event>> Delete(string name);
        
        Task<EventModel> Get(string name);

        Task<IEnumerable<Event>> GetAll();
        Task<IEnumerable<EventModel>> GetAllView();

    }
}
