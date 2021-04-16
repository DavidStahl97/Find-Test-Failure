using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.MicrosoftTeams.Dto
{
    public class CardAction
    {
        [JsonPropertyName("@type")]
        public ActionType Type { get; set; }

        public string Name { get; set; }

        public IEnumerable<Target> Targets { get; set; }
    }
}
