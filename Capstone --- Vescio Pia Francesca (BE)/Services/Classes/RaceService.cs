using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class RaceService : IRacesService
    {
        private readonly DataContext _db;

        public RaceService(DataContext db)
        {
            _db = db;
        }
        public async Task<Race> Create(Race entity)
        {
            try
            {
                await _db.Races.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Race not created", ex);
            }
        }

        public async Task<Race> Delete(int id)
        {

            try
            {
                var race = await Read(id);
                _db.Races.Remove(race);
                await _db.SaveChangesAsync();
                return race;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not deleted", ex);
            }
        }

        public async Task<IEnumerable<Race>> GetAllRaces()
        {
            try
            {
                return await _db.Races.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Races not found", ex);
            }
        }

        public async Task<Race> Read(int id)
        {
            try
            {
                var race = await _db.Races.SingleOrDefaultAsync(i => i.Id == id);

                if (race == null)
                {
                    throw new Exception("Race not found");
                }
                return race;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
        }

        public async Task<Race> Update(Race entity)
        {
            try
            {
                var race = await Read(entity.Id);
                race.Name = entity.Name;
                race.Modifier = entity.Modifier;
                _db.Update(race);
                await _db.SaveChangesAsync();
                return race;
            }
            catch (Exception ex)
            {
                throw new Exception("Race not updated", ex);
            }
        }
    }
}
