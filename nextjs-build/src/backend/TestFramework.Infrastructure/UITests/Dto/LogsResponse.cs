using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.UITests.Dto
{
    public class LogsResponse
    {
        [JsonPropertyName("value")]
        public IEnumerable<Log> Value { get; set; }
    }
}
