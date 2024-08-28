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
        public async Task<EventModel> Create(EventModel model)
        {
            try {

                var newEvent = new Event
                {
                    Name = model.Name,
                    Date = model.Date,
                    YearOfEvent = model.YearOfEvent,
                    Description = model.Description,
                    Influence = model.Influence,
                    Modifier = model.Modifier,
                    Cover = ConvertImage(model.Cover)
                };

                await _db.Events.AddAsync(newEvent);
                await _db.SaveChangesAsync();

                return model;
            } 
            catch(Exception ex) 
            {
                throw new Exception("Create failed", ex);
            }
        }

        public async Task<IEnumerable<Event>> Delete(string name)
        {
            var currEvents = await Read(name);
            foreach (var currEvent in currEvents) {
                _db.Events.Remove(currEvent);
            }
            
            await _db.SaveChangesAsync();
            return currEvents;
        }

        public async Task<EventModel> Get(string name)
        {
                throw new Exception("Error");

        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            try {
                var events = await _db.Events.ToListAsync();
                return events;
            } 
            catch(Exception ex) 
            { 
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<EventModel>> GetAllView()
        { try
            {
                var events = await GetAll();
                var groupedEvents = new List<EventModel>();
                foreach (var e in events)
                {
                    groupedEvents.Add(new EventModel
                    {
                        Name = e.Name,
                        Date = e.Date,
                        YearOfEvent = e.YearOfEvent,
                        Description = e.Description,
                        Influence = e.Influence,
                        Modifier = e.Modifier,
                    });
                }
                    
                return groupedEvents;
            } catch(Exception ex) 
            {
                  throw new Exception("Error", ex); 
            }
           
        }

        public async Task<IEnumerable<Event>> Read(string name)
        { try
            {
                var currEvents = await _db.Events.Where(e => e.Name == name).ToListAsync();
                
                if (currEvents == null) {
                    throw new Exception("Event not found");
                }
                return currEvents;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error", ex);
                    
            }
        }

        public async Task<EventModel> Update(EventModel model)
        {
            throw new Exception();
        }

    }
}
