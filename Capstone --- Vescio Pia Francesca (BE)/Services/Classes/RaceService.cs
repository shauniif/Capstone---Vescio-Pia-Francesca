using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
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
                var characters = await _db.Characters.Where(c => c.Race.Id == id).ToListAsync();
                foreach (var character in characters)
                {
                    _db.Characters.Remove(character);
                }
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
                var races = await _db.Races.ToListAsync();
                return races;
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

        public async Task<RaceDTO> ReturnRace(int id)
        {
            var race = await Read(id);
            var raceDTO = new RaceDTO
            {
                Id = id,
                Name = race.Name,
                Modifier = race.Modifier,
            };
            return raceDTO;
        }
    }
}
