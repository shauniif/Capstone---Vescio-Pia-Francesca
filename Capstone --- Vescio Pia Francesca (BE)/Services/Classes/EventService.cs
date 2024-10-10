using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class EventService : ImageService, IEventService
    {
     
        private readonly DataContext _db;
        private readonly ICharacterService _characterSvc;

        public EventService(DataContext db, ICharacterService characterSvc)
        {
            _db = db;
            _characterSvc = characterSvc;
        }
      
        public async Task<Event> Create(EventModel model)
        {
            try
            {

                var newEvent = new Event
                {
                    Name = model.Name,
                    Date = model.Date,
                    YearOfEvent = model.YearOfEvent,
                    Description = model.Description,
                    Influence = model.Influence,
                    Modifier = model.Modifier,
                    IsChanged = model.IsChanged,
                    Cover = ConvertImage(model.Cover)
                };

                await _db.Events.AddAsync(newEvent);
                await _db.SaveChangesAsync();

                return newEvent;
            }
            catch (Exception ex)
            {
                throw new Exception("Create failed", ex);
            }
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            try
            {
                var events = await _db.Events.ToListAsync();
                
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<Event> Read(int id)
        {
            try
            {
                var currEvent = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
                if (currEvent == null)
                {
                    throw new Exception("Event not found");
                }
                return currEvent;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<Event> Update(EventModel model)
        {
            try
            {
                var currEvent = await Read(model.Id);
                currEvent.Name = model.Name;
                currEvent.Description = model.Description;
                currEvent.Date = model.Date;
                currEvent.Influence = model.Influence;
                currEvent.Modifier = model.Modifier;
                currEvent.IsChanged = model.IsChanged;
                currEvent.Cover = ConvertImage(model.Cover);
                _db.Events.Update(currEvent);
                _db.SaveChanges();
                return currEvent;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }

        }

        public async Task<Event> Delete(int id)
        {
            var currEvent = await Read(id);
            _db.Remove(currEvent);
            await _db.SaveChangesAsync();
            return currEvent;
        }

        public async Task<EventModel> Get(int id)
        {
            try
            {
                var currEvent = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
                if (currEvent == null)
                {
                    throw new Exception("Event not found");
                }
                var eventModel = new EventModel
                {
                    Name = currEvent.Name,
                    Date = currEvent.Date,
                    YearOfEvent = currEvent.YearOfEvent,
                    Description = currEvent.Description,
                    Influence = currEvent.Influence,
                    Modifier = currEvent.Modifier,
                    IsChanged = currEvent.IsChanged ?? false
                };
                return eventModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<Event>> GetEventsOfTheDay()
        {
            try
            {
                var today = DateTime.Today.Date;
                var events = await _db.Events.Where(e => e.Date.Date == today).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<decimal> ChangeModifier(string name, decimal entityModifier, Event currEvent)
        {

            if (name.ToLower().Trim().Contains(currEvent.Influence.ToLower().Trim()))
            {
                entityModifier += currEvent.Modifier;
            }
            
            return entityModifier;
        }

        private async Task UpdateModifiersAsync<T>(IEnumerable<T> entities, Func<T, string> getName, Func<T, decimal> getModifier, Action<T, decimal> setModifier, Event e) where T : BaseEntity
        {
            foreach (var entity in entities)
            {
                var newModifier = await ChangeModifier(getName(entity), getModifier(entity), e);
                setModifier(entity, newModifier);
            }
        }


        public async Task CalcuateModifier(IEnumerable<Eco> ecos, IEnumerable<Guild> guilds, IEnumerable<Nation> nations, IEnumerable<Race> races, IEnumerable<Character> characters)
        {
            var events = await GetEventsOfTheDay();

            foreach (var e in events)
            {
                if (e.IsChanged != null && e.IsChanged == false)
                {
                   
                    await UpdateModifiersAsync(ecos, eco => eco.Name, eco => eco.Modifier, (eco, newModifier) => eco.Modifier = newModifier, e);
                    await UpdateModifiersAsync(guilds, guild => guild.Name, guild => guild.Modifier, (guild, newModifier) => guild.Modifier = newModifier, e);
                    await UpdateModifiersAsync(nations, nation => nation.Name, nation => nation.Modifier, (nation, newModifier) => nation.Modifier = newModifier, e);
                    await UpdateModifiersAsync(races, race => race.Name, race => race.Modifier, (race, newModifier) => race.Modifier = newModifier, e);

                    
                    foreach (var character in characters)
                    {
                        character.Score = await _characterSvc.ChangeScore(character.Id);
                    }

                    
                    e.IsChanged = true;
                }
            }

            await _db.SaveChangesAsync();


        }
    }
}
