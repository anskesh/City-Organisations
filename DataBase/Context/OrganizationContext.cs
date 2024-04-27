using CityOrganisations.Models;
using Microsoft.EntityFrameworkCore;

namespace CityOrganisations.DataBase.Context
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Branch> Branches { get; set; }

        private string _path = "DataBase\\organizations.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"Data Source={_path}");
    }
}