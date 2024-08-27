using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class EcoService : IEcoService
    {
        private readonly DataContext _db;

        public EcoService(DataContext db)
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
        public async Task<Eco> Create(EcoModel entity)
        {
            try
            {
                var nation = await _db.Nations.FirstOrDefaultAsync(n => n.Id == entity.NationId);
                var eco = new Eco
                {
                    Position = entity.Position,
                    Name = entity.Name,
                    Description = entity.Description,
                    Modifier = entity.Modifier,
                    Photo = ConvertImage(entity.Photo),
                    Nation = nation,
                };
                await _db.Ecos.AddAsync(eco);
                await _db.SaveChangesAsync();
                return eco;
            }
            catch (Exception ex) 
            {
                throw new Exception("Eco not created", ex);
            }
        }

        public async Task<Eco> Delete(int id)
        {
            try
            {
                var eco = await Read(id);
                _db.Ecos.Remove(eco);
                await _db.SaveChangesAsync();
                return eco;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete failed", ex);
            }
        }

        public async Task<EcoModel> Get(int id)
        { try
            {

            
            var eco = await _db.Ecos
                .Include(x => x.Nation)
                .FirstOrDefaultAsync(e => e.Id == id);
                if (eco == null)
            {
                throw new Exception("Eco not found");
            }
            var nation = new Nation();
            if(eco.Nation != null)
                {
                  nation = await _db.Nations.FirstOrDefaultAsync(n => n.Id == eco.Nation.Id);
                }

                var ecoModel = new EcoModel
                {
                    Id = eco.Id,
                    Position = eco.Position,
                    Name = eco.Name,
                    Description = eco.Description,
                    Modifier = eco.Modifier,
                    NationId = nation.Id
                };
            return ecoModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
        }

        public async Task<IEnumerable<Eco>> GetAllEcos()
        {
            try
            {
                return await _db.Ecos
                    .Include(e =>  e.Nation)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ecos not found", ex);
            }
        }

        public async Task<Eco> Read(int id)
        {
            try
            {
                var eco = await _db.Ecos
                .Include(x => x.Nation)
                .FirstOrDefaultAsync(e => e.Id == id);
                if (eco == null)
                {
                    throw new Exception("Eco not found");
                }
                return eco;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }

        }

        public async Task<Eco> Update(EcoModel entity)
        {
            try
            {
                var nation = await _db.Nations.FirstOrDefaultAsync(n => n.Id == entity.NationId);
                var eco = await Read(entity.Id);
                eco.Position = entity.Position;
                eco.Name = entity.Name;
                eco.Description = entity.Description;
                eco.Photo = ConvertImage(entity.Photo);
                eco.Modifier = entity.Modifier;
                eco.Nation = nation;
                _db.Ecos.Update(eco);
                await _db.SaveChangesAsync();
                return eco;
            }
            catch (Exception ex)
            {
                throw new Exception("Update failed", ex);
            }
        }

        
    }
}
