using Microsoft.EntityFrameworkCore;
using RefuelingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary;

namespace FuelServerConsolApp
{
    public class FuelDbContext : DbContext
    {
        DbSet<Person> Persons { get; set; }
        DbSet<Track> Tracks { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Refuel> Refuels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=fuel_db;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
