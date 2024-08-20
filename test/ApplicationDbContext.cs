namespace test
{
    using Microsoft.EntityFrameworkCore;
    using test.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<State> States { get; set; }

        public DbSet<Municipality> Municipalities { get; set; }

        public DbSet<Parish> Parishes { get; set; }

        public DbSet<Center> Centers { get; set; }

        public DbSet<PollingStation> PollingStations { get; set; }
    }
}