using iosha.RaspberryPi3WebControl.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace iosha.RaspberryPi3WebControl.Storage
{
    public class RaspberryPi3WebControlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=RaspberryPi3WebControlDb.db");
        }

        public DbSet<LedSchema> LedSchemas { get; set; }
        public DbSet<LedItem> LedItems { get; set; }

    }
}
