using FluentAssertions;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.UnitTests
{
    public class ValidateJson
    {
        public static void ShouldBeEquivalent(string actual, string expected)
        {
            var jsonActual = JToken.Parse(actual);
            var jsonExpected = JToken.Parse(expected);
            jsonActual.Should().BeEquivalentTo(jsonExpected);
        }
    }
}
