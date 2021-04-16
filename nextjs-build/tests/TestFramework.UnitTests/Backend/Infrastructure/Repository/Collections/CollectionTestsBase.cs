using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository;
using TestFramework.Infrastructure.Repository;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.UnitTests.Backend.Infrastructure.Repository.Collections
{
    public class CollectionTestsBase<TEntity>
        where TEntity : class
    {
        private readonly RepositoryContext _context;

        public CollectionTestsBase(ILogger<TestFramework.Infrastructure.Repository.Repository> logger)
        {
            Fixture = FixtureFactory.GetCustomizedFixture();

            _context = new RepositoryContextMock();
            Seed();

            Repository = new TestFramework.Infrastructure.Repository.Repository(_context, logger);
        }

        protected IFixture Fixture { get; }

        protected IRepository Repository { get; }

        protected async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        private void Seed()
        {                        
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
