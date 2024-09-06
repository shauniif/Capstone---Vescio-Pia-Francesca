using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class GuildService : IGuildService
    {
        private readonly DataContext _db;

        public GuildService(DataContext db)
        {
            _db = db;
        }
        public async Task<Guild> Create(GuildModel entity)
        {
            try
            {
                var nation = await _db.Nations.FirstOrDefaultAsync(n => n.Id == entity.NationId);
                if (nation == null)
                {
                    throw new Exception("Nation not found");
                }
                var guild = new Guild
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    Modifier = entity.Modifier,
                    Nation = nation
                };
                await _db.Guilds.AddAsync(guild);
                await _db.SaveChangesAsync();
                return guild;
            }
            catch (Exception ex)
            {
                throw new Exception("Guild not created", ex);
            }
        }

        public async Task<Guild> Delete(int id)
        {   try
            {
                var guild = await Read(id);
                if (guild == null)
                {
                    throw new Exception("Guild not found");
                }
                _db.Guilds.Remove(guild);

                var characters = await _db.Characters.Where(c => c.Guild.Id == id).ToListAsync();
                foreach (var character in characters)
                {
                    character.Guild = null;
                }
                await _db.SaveChangesAsync();
                return guild;
            }
            catch (Exception ex) 
            {
                throw new Exception("Delete failed", ex);
            }
        }

        public async Task<GuildModel> Get(int id)
        {
            try {
                var guild = await _db.Guilds
                    .Include(g => g.Nation)
                    .SingleOrDefaultAsync(i => i.Id == id);
                if (guild == null)
                {
                    throw new Exception("Guild not found");
                }
                var guildModel = new GuildModel
                {
                    Name = guild.Name,
                    Description = guild.Description,
                    Modifier = guild.Modifier,
                    NationId = guild.Nation.Id,
                };
                return guildModel;
            } catch(Exception ex) {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<Guild>> GetAll()
        {
            try 
            {
                return await _db.Guilds
                    .Include(g => g.Nation)
                    .ToListAsync();
            } catch(Exception ex) 
            {
                throw new Exception("Guilds not found", ex);
            };
        }

        public async Task<Guild> GetGuild(int id)
        {
            var guild = await _db.Guilds
                       .Include(g => g.Nation)
                       .Select(g => new Guild
                       {
                           Id = g.Id,
                           Name = g.Name,
                           Description = g.Description,
                           Modifier = g.Modifier,
                           Nation = new Nation
                           {
                               Id = g.Nation.Id,
                               Name = g.Nation.Name,
                           }
                       }).FirstOrDefaultAsync(c => c.Id == id);
            if (guild == null)
            {
                throw new Exception("Error");
            }
            return guild;
        }

        public async Task<Guild> Read(int id)
        {
            try
            {
                var guild = await _db.Guilds
                    .Include(g => g.Nation)
                    .SingleOrDefaultAsync(i => i.Id == id);

                if (guild == null)
                {
                    throw new Exception("Guild not found");
                }
                return guild;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
        }

        public async Task<Guild> Update(GuildModel entity)
        { try
            {
                var nation = await _db.Nations.FirstOrDefaultAsync(n => n.Id == entity.NationId);
                if (nation == null)
                {
                    throw new Exception("Nation not found");
                }
                var guild = await Read(entity.Id);
                guild.Name = entity.Name;
                guild.Description = entity.Description;
                guild.Modifier = entity.Modifier;
                guild.Nation = nation;
                _db.Update(guild);
                await _db.SaveChangesAsync();
                return guild;
            }
            catch (Exception ex) 
            {
                throw new Exception("Update failed", ex);
            }
            
        }

        public async Task<IEnumerable<Guild>> Search(string query)
        {
            var guilds = await _db.Guilds.Include(g => g.Nation)
                .Where(
                g => g.Name.Contains(query) ||
                g.Description.Contains(query) ||
                g.Nation.Name.Contains(query)
                ).ToListAsync();

            var guildsSel = new List<Guild>();
            foreach (var guild in guilds)
            {
                var guildSel = await GetGuild(guild.Id);
                guildsSel.Add(guildSel);
            }
            return guildsSel;
        }
    }
}
