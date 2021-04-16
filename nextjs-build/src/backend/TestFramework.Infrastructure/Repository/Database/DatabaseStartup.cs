using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Database
{
    public static class DatabaseStartup
    {
        public static DbContextOptionsBuilder ConfigureDatabaseOptions(DbContextOptionsBuilder options,
            IConfiguration configuration)
            => options.UseMySQL(configuration.GetConnectionString("Database"));

        public static void Migrate(IConfiguration configuration)
        {
            Log.Information("Migrate Database");

            var optionBuilder = new DbContextOptionsBuilder();
            var options = ConfigureDatabaseOptions(optionBuilder, configuration).Options;

            using var dbContext = new RepositoryContext(options);
            dbContext.Database.Migrate();
        }
    }
}
