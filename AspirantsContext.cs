using Microsoft.EntityFrameworkCore;

namespace PrincessConsole
{
    public class AspirantsContext: DbContext
    {
        public DbSet<AspirantData> Aspirants { get; set; }
        public DbSet<Attempt> Attempts { get; set; }

        private int MaxAspirantNameLength = 50;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=localhost;Database=TestAppDB;User Id=postgres;Password=587963klop";
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspirantData>().Property(a => a.Id).ValueGeneratedNever();
            modelBuilder.Entity<AspirantData>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<AspirantData>().Property(a => a.Quality).IsRequired();
            modelBuilder.Entity<AspirantData>().Property(a => a.Name).HasMaxLength(MaxAspirantNameLength);
            modelBuilder.Entity<Attempt>().Property(a => a.Id).ValueGeneratedNever();
            modelBuilder.Entity<Attempt>().Property(a => a.Aspirants).IsRequired();
        }
    }
}