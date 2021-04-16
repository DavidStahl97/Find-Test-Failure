using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        bool open = false;

        void TogglDrawer()
        {
            open = !open;
        }
    }
}
