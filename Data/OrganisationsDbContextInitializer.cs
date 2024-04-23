using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOrganisations.Data
{
    class OrganisationsDbContextInitializer : SqliteDropCreateDatabaseAlways<OrganisationsDbContext>
    {
        public OrganisationsDbContextInitializer(DbModelBuilder modelBuilder) : base(modelBuilder) { }

        protected override void Seed(OrganisationsDbContext context)
        {

        }
    }
}
