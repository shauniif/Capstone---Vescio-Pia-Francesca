using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Context
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Nation> Nations { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Eco> Ecos { get; set; }
        public virtual DbSet<Guild> Guilds { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<GuildCharacterRole> GuildRole {  get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Roles)
                .WithMany(i => i.Users)
                .UsingEntity(j => j.ToTable("RoleUsers"));
        }
    }
    }
