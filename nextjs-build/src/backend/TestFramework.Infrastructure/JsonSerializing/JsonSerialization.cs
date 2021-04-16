using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure
{
    public class JsonSerialization : IJsonSerialization
    {
        private static readonly JsonSerializerOptions _serializationOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        public string Serialize<T>(T entity)
            => JsonSerializer.Serialize(entity, options: _serializationOptions);

        public T Deserialize<T>(string s)
            => JsonSerializer.Deserialize<T>(s);
    }
}
