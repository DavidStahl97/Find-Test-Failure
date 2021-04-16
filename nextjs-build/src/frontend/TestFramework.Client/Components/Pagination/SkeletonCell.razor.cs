using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Components
{
    public partial class SkeletonCell : ComponentBase
    {
        [Parameter]
        public int Width { get; set; }
    }
}
