using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Data
{
    public class Capstone_____Vescio_Pia_Francesca__BE_Context : DbContext
    {
        public Capstone_____Vescio_Pia_Francesca__BE_Context (DbContextOptions<Capstone_____Vescio_Pia_Francesca__BE_Context> options)
            : base(options)
        {
        }

        public DbSet<Capstone_____Vescio_Pia_Francesca__BE_.Entity.Nation> Nation { get; set; } = default!;
        public DbSet<Capstone_____Vescio_Pia_Francesca__BE_.Entity.Event> Event { get; set; } = default!;
    }
}
