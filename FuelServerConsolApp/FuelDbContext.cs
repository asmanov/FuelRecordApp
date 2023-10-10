using Microsoft.EntityFrameworkCore;
using RefuelingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary;
using FuelResponseLibrary;

namespace FuelServerConsolApp
{
    public class FuelDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Refuel> Refuels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=fuel_db;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
