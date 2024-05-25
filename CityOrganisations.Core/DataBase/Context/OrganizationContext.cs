using CityOrganisations.Configuration;
using Core.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DataBase
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Branch> Branches { get; set; }

        private string? _path;
        private ConfigurationService _configurationService;

        public OrganizationContext(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _path = _configurationService.Configuration.DataBasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"Data Source={_path}");
    }
}