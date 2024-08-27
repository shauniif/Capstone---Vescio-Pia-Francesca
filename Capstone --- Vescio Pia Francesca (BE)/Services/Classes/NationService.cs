﻿using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class NationService : INationService
    {
        private readonly DataContext _db;

        public NationService(DataContext db)
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

       


        public async Task<Nation> Create(NationModel entity)
        {
            try
            {
                var nation = new Nation
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    FormOfGovernment = entity.FormOfGovernment,
                    Modifier = entity.Modifier,
                    Photo = ConvertImage(entity.Photo),
                };
                await _db.Nations.AddAsync(nation);
                await _db.SaveChangesAsync();
                return nation;
            }
            catch (Exception ex)
            {
                throw new Exception("Nation not created", ex);
            }
        }

        public async Task<Nation> Delete(int id)
        {
            try
            {
                var nation = await Read(id);
                _db.Nations.Remove(nation);
                var ecos = await _db.Ecos.Where(e => e.Nation.Id == id).ToListAsync();
                var cities = await _db.Cities.Where(c => c.Nation.Id == id).ToListAsync();
                var guilds = await _db.Guilds.Where(c => c.Nation.Id == id).ToListAsync();

                // In questo modo gli Echi (che sono associati a quella determinata nazione non vengono eleminati, ma non sono più associati a nessuna nazione
                // in questo modo sono "NOMADI"
                foreach (var eco in ecos)
                {
                    eco.Nation = null;
                }
                foreach (var guild in guilds) { 
                    _db.Remove(guild);
                }
                foreach(var city in cities )
                {
                    _db.Remove(city); 
                }
                await _db.SaveChangesAsync();
                return nation;
            }
            catch (Exception ex) 
            {
                throw new Exception("Delete failed", ex);
            }
        }

        public async Task<IEnumerable<Nation>> GetAllNations()
        {
            try
            {
                return await _db.Nations.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Nations not found", ex);
            }
        }

        public async Task<Nation> Read(int id)
        {
            try
            {
                var nation = await _db.Nations
                    .Include(n => n.Cities)
                    .SingleOrDefaultAsync(i => i.Id == id);

                if (nation == null)
                {
                    throw new Exception("Nation not found");
                }
                return nation;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }

        }
        

        public async Task<Nation> Update(NationModel entity)
        {
            try
            {
                var nation = await Read(entity.Id);
                nation.Name = entity.Name;
                nation.Description = entity.Description;
                nation.FormOfGovernment = entity.FormOfGovernment;
                nation.Photo = ConvertImage(entity.Photo);

                _db.Nations.Update(nation);
                await _db.SaveChangesAsync();
                return nation;
            } catch (Exception ex) 
            {
                  throw new Exception("Update failed", ex); 
            }
          
        }

        public async Task<NationModel> Get(int id)
        {
            try
            {
                var nation = await _db.Nations
                    .SingleOrDefaultAsync(i => i.Id == id);

                if (nation == null)
                {
                    throw new Exception("Nation not found");
                }
                var nationModel = new NationModel();
                nationModel.Name = nation.Name;
                nationModel.Description = nation.Description;
                nationModel.FormOfGovernment = nation.FormOfGovernment;
                nationModel.Modifier = nation.Modifier;
                nationModel.Cities = nation.Cities;
                return nationModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
        }
    }
}