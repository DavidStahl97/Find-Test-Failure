using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database
{

    // dotnet ef database update --project src/backend/TestFramework.Infrastructure/ --startup-project src/backend/TestFramework.WebAPI/
    // dotnet ef migrations add EventLogs --output-dir Repository/Database/Migrations --project src/backend/TestFramework.Infrastructure/ --startup-project src/backend/TestFramework.WebAPI/
    // dotnet ef migrations remove Initial
    public class RepositoryContext : DbContext
    {
        public RepositoryContext([NotNull] DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<UIElement> UIElements { get; set; }

        public DbSet<UIEvent> UIEvents { get; set; }

        public DbSet<UITestCase> UITestCases { get; set; }

        public DbSet<UITestRun> UITestRuns { get; set; }

        public DbSet<UITestRunCase> UITestRunCases { get; set; }

        public DbSet<UITestRunUIElement> UITestRunUIElement { get; set; }

        public DbSet<Page> Page { get; set; }

        public DbSet<HealthCheck> HealthChecks { get; set; }

        public DbSet<UserFile> UserFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
