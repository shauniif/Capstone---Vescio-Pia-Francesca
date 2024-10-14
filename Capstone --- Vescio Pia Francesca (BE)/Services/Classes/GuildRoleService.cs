using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using System;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class GuildRoleService : IGuildRoleService
    {
        private readonly DataContext _db;

        public GuildRoleService(DataContext db)
        {
            _db = db;
        }
        public async Task<GuildRole> Create(GuildRoleDTO entity)
        {
            try
            {
                var guild = await _db.Guilds.FirstOrDefaultAsync(g => g.Id == entity.GuildId);
                if (guild == null)
                {
                    throw new Exception("Guild not found");
                }
                var role = new GuildRole
                {
                    Name = entity.Name,
                    Modifier = entity.Modifier,
                    Guild = guild
                };




                _db.GuildRole.Add(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not created", ex);
            }
        }

        public async Task<GuildRole> Delete(int id)
        {
            try
            {
                var role = await Read(id);
                _db.GuildRole.Remove(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not found", ex);
            }
        }

        public async Task<IEnumerable<GuildRole>> GetAll()
        {
            try
            {
                return await _db.GuildRole
                    .Include(gr => gr.Guild)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Role not found", ex);
            }
        }

        public async Task<GuildRole> Read(int id)
        {
            try
            {
                var role = await _db.GuildRole.SingleOrDefaultAsync(i => i.Id == id);
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not found", ex);

            }
        }

        public async Task<GuildRole> Update(GuildRoleDTO entity)
        {
            try
            {
                var role = await Read(entity.Id);
                role.Name = entity.Name;
                role.Modifier = entity.Modifier;
                _db.GuildRole.Update(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not updated", ex);
            }
        }
    }
}
