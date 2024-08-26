using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class CitiesService : ICitiesService
    {
        private readonly DataContext _db;

        public CitiesService(DataContext db)
        {
            _db = db;
        }
        public Task<City> Create(City entity)
        {
            throw new NotImplementedException();
        }

        public async Task<City> Delete(int id)
        {
            try
            {
                var city = await Read(id);
                _db.Cities.Remove(city);
                await _db.SaveChangesAsync();
                return city;
            }
            catch (Exception ex) 
            {
                throw new Exception("Delete failed", ex);
            }
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            try
            {
                return await _db.Cities
                    .Include(c => c.Nation)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Cities not found", ex);
            }
        }

        public async Task<City> Read(int id)
        {
            try
            {
                var nation = await _db.Cities.SingleOrDefaultAsync(i => i.Id == id);

                if (nation == null)
                {
                    throw new Exception("City not found");
                }
                return nation;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
        }

        public async Task<City> Update(City entity)
        {
            try
            {
                var city = await Read(entity.Id);
                city.Name = entity.Name;
                city.Description = entity.Description;
                city.Nation = entity.Nation;
                _db.Update(city);
                await _db.SaveChangesAsync();
                return city;
            }
            catch (Exception ex)
            {
                throw new Exception("City not updated", ex);
            }
        }
    }
}
