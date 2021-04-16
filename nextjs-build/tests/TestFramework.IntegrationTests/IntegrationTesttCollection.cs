using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFramework.IntegrationTests
{
    [CollectionDefinition(nameof(IntegrationTesttCollection))]
    public class IntegrationTesttCollection : ICollectionFixture<IntegrationTestFixture>
    {
    }
}
