﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;
using DesignGenerator.Infrastructure.DBEntities;
using DesignGenerator.Infrastructure.Database.DBEntities;

namespace DesignGenerator.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        string connectionString;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;
        public DbSet<Illustration> Illustrations { get; set; } = null!;
        public DbSet<Prompt> Prompts { get; set; } = null!;

        public ApplicationDbContext(IConfiguration config)
        {
            connectionString = config.GetConnectionString("SQLLiteConnection") 
                ?? throw new Exception("Connection string was not found. Name: SQLLiteConnection");

            Database.EnsureCreated();
        }

        public ApplicationDbContext()
        {
            connectionString = "Data Source=C:\\Users\\Ishtar\\Институт\\Диплом\\DesignGeneratorUI\\DesignGenerator.Infrastructure\\app.db";//Host=localhost;Port=5432;Database=bank;Username=kamish;Password=12345";

            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseSerialColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
