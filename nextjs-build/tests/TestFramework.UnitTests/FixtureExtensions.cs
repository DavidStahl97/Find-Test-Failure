using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.UnitTests
{
    public static class FixtureExtensions
    {
        public static string CreateString(this IFixture fixture, int max)
        {
            var randomString = fixture.Create<string>();
            while (randomString.Length < max)
            {
                randomString += randomString;
            }
            return randomString.Substring(0, max);
        }
    }
}
