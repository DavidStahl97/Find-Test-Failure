using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestFramework.Infrastructure.JsonSerializing.Converter;

namespace TestFramework.Infrastructure.UITests.Dto
{
    public class Log
    {
        [JsonPropertyName("level")]
        public string Level { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }        

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(NullableDateTimeTimestampConverter))]
        public DateTime? Timestamp { get; set; }
    }
}
