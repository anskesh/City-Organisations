using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

namespace CityOrganisations.Data
{
    class Configuration : DbConfiguration
    {
        public Configuration()
        {
            SetProviderFactory("System.data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.data.SQLite.EF6", SQLiteProviderFactory.Instance);

            SetProviderFactory("System.data.SQLite", SQLiteProviderFactory.Instance);
            SetProviderFactory("System.data.SQLite.EF6", SQLiteProviderFactory.Instance);
        }

        public DbConnection CreateConnection(string connectionString)
            => new SQLiteConnection(connectionString);
    }
}
