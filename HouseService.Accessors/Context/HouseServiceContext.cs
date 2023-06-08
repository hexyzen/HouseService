using HouseService.Accessors.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseService.Accessors.Context
{
    public class HouseServiceContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Dog> Dogs { get; set; } = null!;

        public HouseServiceContext()
        {

        }
        public HouseServiceContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=shorturlapidb;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
