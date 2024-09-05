using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _db;

        public CharacterService(DataContext db)
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

        public async Task<Character> Create(CharacterDTO dto)
        {
            try
            {
                Eco eco = null;
                Guild guild = null;
                if (dto.GuildId != null) {
                     guild = await _db.Guilds.FirstOrDefaultAsync(g => g.Id == dto.GuildId);
                    if (guild == null)
                    {
                        throw new Exception("Guild not found");
                    }
                }
                
                if(dto.EcoId != null)
                {
                    eco = await _db.Ecos.FirstOrDefaultAsync(e => e.Id == dto.EcoId);
                    if (eco == null)
                    {
                        throw new Exception("Eco not found");
                    }
                }
                
                var city = await _db.Cities.FirstOrDefaultAsync(c => c.Id == dto.CityId);
                if (city == null)
                {
                    throw new Exception("City not found");
                }


                var race = await _db.Races.FirstOrDefaultAsync(r => r.Id == dto.RaceId);
                if (race == null)
                {
                    throw new Exception("Race not found");
                }

                var user = await _db.Users.FirstAsync(u => u.Id == dto.UserId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                var character = new Character
                {
                    Name = dto.Name,
                    City = city,
                    Race = race,
                    Eco = eco,
                    Guild = guild,
                    User = user,
                    Image = ConvertImage(dto.Image),
                    Score = await CountScore(eco?.Modifier ?? 1, city.Id, race.Modifier, guild?.Modifier ?? 1)
                };

                await _db.Characters.AddAsync(character);
                await _db.SaveChangesAsync();
                return character;

            }
            catch (Exception ex) 
            {
                throw new Exception("Create failed", ex);
            }
        }

        public async Task<Character> Delete(int id)
        {
            var character = await Read(id);
            _db.Characters.Remove(character);
            await _db.SaveChangesAsync();
            return character;
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            try
            {
                var comments = await _db.Characters
                     .Include(c => c.Guild)
                        .Include(c => c.City)
                        .Include(c => c.Race)
                        .Include(c => c.Eco)
                        .Include(c => c.User)
                    .ToListAsync();
                return comments;
            }
            catch (Exception ex)
            {
                throw new Exception("Comments not found", ex);
            }
        }

        public async Task<IEnumerable<Character>> GetAllByUser(int id)
        {
            try
            {
                var comments = await _db.Characters
                     .Include(c => c.Guild)
                        .Include(c => c.City)
                        .Include(c => c.Race)
                        .Include(c => c.Eco)
                        .Include(c => c.User)
                    .Where(c => c.User.Id == id)
                    .ToListAsync();
                return comments;
            }
            catch (Exception ex)
            {
                throw new Exception("Comments not found", ex);
            }
        }

        public async Task<Character> Read(int id)
        {
            var character = await SelectCharacter(id);
            return character;
        }

        public async Task<Character> Update(CharacterDTO dto)
        {
            try
            {
                var eco = new Eco();
                var guild = new Guild();

                var currCharacter = await _db.Characters.FirstOrDefaultAsync(c => c.Id == dto.Id);
                if (currCharacter == null)
                {
                    throw new Exception("Comment not found");
                }

                if (dto.GuildId != null)
                {
                    guild = await _db.Guilds.FirstOrDefaultAsync(g => g.Id == dto.GuildId);
                    if (guild == null)
                    {
                        throw new Exception("Guild not found");
                    }
                }

                if (dto.EcoId != null)
                    {
                        eco = await _db.Ecos.FirstOrDefaultAsync(e => e.Id == dto.EcoId);
                        if (eco == null)
                        {
                            throw new Exception("Eco not found");
                        }
                    }

                var city = await _db.Cities.FirstOrDefaultAsync(c => c.Id == dto.CityId);
                if (city == null)
                {
                    throw new Exception("City not found");
                }


                var race = await _db.Races.FirstOrDefaultAsync(r => r.Id == dto.RaceId);
                if (race == null)
                {
                    throw new Exception("Race not found");
                }
                currCharacter.Name = dto.Name;
                currCharacter.Race = race;
                currCharacter.City = city;
                currCharacter.Guild = guild;
                currCharacter.Eco = eco;
                currCharacter.Image = ConvertImage(dto.Image);
                currCharacter.Score = await CountScore(eco?.Modifier ?? 1, city.Id, race.Modifier, guild?.Modifier ?? 1);
                _db.Characters.Update(currCharacter);
                await _db.SaveChangesAsync();

                return await SelectCharacter(currCharacter.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Update failed", ex);
            }
        }

        public async Task<Character> SelectCharacter(int id)
        {
            try
            {
                var character = await _db.Characters
                        .Include(c => c.Guild)
                        .Include(c => c.City)
                        .Include(c => c.Race)
                        .Include(c => c.Eco)
                        .Include(c => c.User)
                        .Where(c => c.Id == id)
                        .Select(c => new Character
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Image = c.Image,
                            Score = c.Score,
                            Guild = c.Guild != null ? new Guild
                            {
                                Id = c.Guild.Id,
                                Name = c.Guild.Name,
                                Modifier = c.Guild.Modifier,
                            } : null,
                            Eco = c.Eco != null ? new Eco
                            {
                                Id = c.Eco.Id,
                                Name = c.Eco.Name,
                                Modifier = c.Eco.Modifier,
                            } : null,
                            City = new City
                            {
                                Id = c.City.Id,
                                Name = c.City.Name,
                            },
                            Race = new Race
                            {
                                Id = c.Race.Id,
                                Name = c.Race.Name,
                                Modifier = c.Race.Modifier,
                            },
                            User = new User
                            {
                                Id = c.User.Id,
                                FirstName = c.User.FirstName,
                                LastName = c.User.LastName,
                                Email = c.User.Email,
                                DateBirth = c.User.DateBirth,
                                Username = c.User.Username
                            }
                        })
                        .FirstOrDefaultAsync();
                if (character == null)
                {
                    throw new Exception("Character not found");
                }

                return character;
            }
            catch (Exception ex) 
            {
                throw new Exception("Character not found", ex);
            }
        }

        public async Task<decimal> CountScore(decimal ecoModifier, int cityid, decimal raceModifier, decimal guildModifier)
        {
            var nation = await _db.Nations.Include(n => n.Cities)
                .Where(n => n.Cities.Any(c => c.Id == cityid))
                .FirstOrDefaultAsync();
            if (nation == null)
            {
                throw new Exception("Nation not found");
            }
            var score = 10 * ecoModifier * nation.Modifier * raceModifier * guildModifier;
            return score;
        }

        public async Task<decimal> ChangeScore(int id)
        {
            var character = await Read(id);

            var characterScoreChanged = await CountScore(character.Eco?.Modifier ?? 1, character.City.Id, character.Race.Modifier, character.Guild?.Modifier ?? 1);

            character.Score = characterScoreChanged;
            await _db.SaveChangesAsync();
            return characterScoreChanged;
        }
    }
}
