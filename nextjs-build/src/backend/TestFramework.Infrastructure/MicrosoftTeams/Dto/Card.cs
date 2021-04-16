using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.MicrosoftTeams.Dto
{
    public class Card
    {
        [JsonPropertyName("@type")]
        public MessageType Type { get; set; } = MessageType.MessageCard;

        [JsonPropertyName("@context")]
        public string Context { get; set; } = "http://schema.org/extensions";

        public string ThemeColor { get; set; }

        public string Summary { get; set; }

        public IEnumerable<Section> Sections { get; set; }

        public IEnumerable<CardAction> PotentialAction { get; set; }
    }
}
