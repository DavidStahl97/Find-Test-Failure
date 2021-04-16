using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.UITests.Dto
{
    public class LogRequest
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
