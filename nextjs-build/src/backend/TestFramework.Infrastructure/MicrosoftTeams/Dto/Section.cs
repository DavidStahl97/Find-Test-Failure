using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.MicrosoftTeams.Dto
{
    public class Section
    {
        public string ActivityTitle { get; set; }

        public bool Markdown { get; set; } = true;

        public IEnumerable<Fact> Facts { get; set; }
    }
}
