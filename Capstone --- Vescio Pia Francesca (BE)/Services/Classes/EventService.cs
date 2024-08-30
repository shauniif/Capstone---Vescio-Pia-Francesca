using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class EventService : IEventService
    {
     
        private readonly DataContext _db;

        public EventService(DataContext db)
        {
            _db = db;
        }
        private string ConvertImage(IFormFile image)
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);
                string urlImg = $"data:image/jpeg;base64,{base64String}";
                return urlImg;
            }

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
                    IsChanged = false,
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
                var events = await _db.Events.Where(e => e.Date == today).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<decimal> ChangeModifier(string name, decimal modifier)
        {
            var events = await GetEventsOfTheDay();

            foreach (var e in events)
            {
                if(e.IsChanged != null)
                {

                if ( e.IsChanged == false&& e.Influence.ToLower().Trim().Contains(name.ToLower().Trim()))
                {
                    modifier += e.Modifier;
                    e.IsChanged = true;
                }
                }
            }
            await _db.SaveChangesAsync();
            return modifier;
        }
    }
}
