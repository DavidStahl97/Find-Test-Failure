using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.MicrosoftTeams
{
    public class MicrosoftTeamsApiOptions
    {
        public const string Position = "MicrosoftTeamsApi";

        public Uri Uri { get; set; }

        public Uri WebApiBaseUri { get; set; }

        public int MaxRetries { get; set; }

        public int WaitInMilliseconds { get; set; }
    }
}
