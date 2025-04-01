using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesignGenerator.Infrastructure.DBEntities;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;

namespace DesignGenerator.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        string connectionString;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Pattern> Patterns { get; set; } = null!;
        public DbSet<Illustration> Illustrations { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public ApplicationDbContext(IConfiguration config)
        {
            connectionString = config.GetConnectionString("PostgresConnection") 
                ?? throw new Exception("Connection string was not found. Name: PostgresConnection");

            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
