using Microsoft.EntityFrameworkCore;
using time_of_your_life.Infrastructure.Data.Entities;

namespace time_of_your_life.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<ClockProps> ClockPresets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
