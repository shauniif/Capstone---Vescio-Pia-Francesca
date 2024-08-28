using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _db;

        public RoleService(DataContext db)
        {
            _db = db;
        }
        public async Task<Role> Create(Role entity)
        {
            try
            {
                _db.Roles.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not created", ex);
            }
        }

        public async Task<Role> Delete(int id)
        {
            try
            {
                var role = await Read(id);
                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Ingredients not found", ex);
            }
        }

        public async Task<Role> Read(int id)
        {
            try
            {
                var role = await _db.Roles.SingleOrDefaultAsync(i => i.Id == id);
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Ingredient not found", ex);

            }
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            try
            {
                return await _db.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ingredients not found", ex);
            }
        }

        public async Task<Role> Update(Role entity)
        {
            try
            {
                var role = await Read(entity.Id);
                role.Name = entity.Name;
                _db.Update(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Ingredient not updated", ex);
            }
        }

        public async Task<Role> RoleAdmin()
        {
            var roleAdmin = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (roleAdmin == null)
            {
                throw new Exception("Role Admin not found");
            }
            return roleAdmin;
        }
    }
}

