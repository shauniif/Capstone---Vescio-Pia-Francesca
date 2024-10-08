﻿using Capstone_____Vescio_Pia_Francesca__BE_.Context;
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
                var characters = await _db.Characters.Where(c => c.City.Id == id).ToListAsync();
                foreach (var character in characters)
                {
                    _db.Characters.Remove(character);
                }
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

        public async Task<IEnumerable<City>> GetAll()
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

        public async Task<City> GetCity(int id)
        {
            var city = await _db.Cities
                       .Include(c => c.Nation)
                       .Select(c => new City
                       {
                           Id = c.Id,
                           Name = c.Name,
                           Description = c.Description,
                           Nation = new Nation
                           {
                               Id = c.Nation.Id,
                               Name = c.Nation.Name,
                           } 
                       }).FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
            {
                throw new Exception();
            }
            return city;
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

        public async Task<IEnumerable<City>> Search(string query)
        {
            var cities = await _db.Cities.Include(g => g.Nation)
                .Where(
                g => g.Name.Contains(query) ||
                g.Description.Contains(query) ||
                g.Nation.Name.Contains(query)
                ).ToListAsync();

            var citiesSel = new List<City>();
            foreach (var city in cities)
            {
                var citySel = await GetCity(city.Id);
                citiesSel.Add(citySel);
            }
            return citiesSel;
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
