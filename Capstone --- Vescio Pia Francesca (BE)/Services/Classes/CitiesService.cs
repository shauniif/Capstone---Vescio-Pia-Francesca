using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
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
        public async Task<City> Create(CityModel entity)
        {
            try
            {
                var nation = await _db.Nations.FirstOrDefaultAsync(n => n.Id == entity.NationId);
                if (nation == null) {
                    throw new Exception("Nation not found");
                }
                var city = new City
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    Nation = nation
                };
                await _db.Cities.AddAsync(city);
                await _db.SaveChangesAsync();
                return city;
            }
            catch (Exception ex)
            {
                throw new Exception("City not created", ex);
            }
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

        public async Task<CityModel> Get(int id)
        {
            try
            {
                var city = await _db.
                    Cities
                    .Include(c => c.Nation)

                    .SingleOrDefaultAsync(i => i.Id == id);

                if (city == null)
                {
                    throw new Exception("City not found");
                }
                var cityModel = new CityModel
                {
                    Name = city.Name,
                    Description = city.Description,
                    NationId = city.Nation.Id
                };
                return cityModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

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
                var city = await _db.Cities.SingleOrDefaultAsync(i => i.Id == id);

                if (city == null)
                {
                    throw new Exception("City not found");
                }
                return city;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
        }

        public async Task<City> Update(CityModel entity)
        {
            try
            {
                var nation = await _db.Nations.FirstOrDefaultAsync(n => n.Id == entity.NationId);
                if (nation == null)
                {
                    throw new Exception("Nation not found");
                }
                var city = await Read(entity.Id);
                city.Name = entity.Name;
                city.Description = entity.Description;
                city.Nation = nation;
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
