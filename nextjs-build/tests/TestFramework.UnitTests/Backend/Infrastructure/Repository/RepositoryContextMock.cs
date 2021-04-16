using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.UnitTests.Backend.Infrastructure.Repository
{
    public class RepositoryContextMock : RepositoryContext
    {
        public RepositoryContextMock()
            : base(GetDbContextOptions())
        { 
        }

        private static DbContextOptions GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<RepositoryContext>()
                .UseMySQL("Server=localhost,15789; Database=TestFramework-UnitTests; User=sa; Password=Your_password123;")
                .Options;
        }
    }
}
