using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Pages.TestCases.EditEvents;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class Events : ComponentBase
    {
        private bool _previousEditing;
        private bool _shouldRender;

        [Parameter]
        public IEnumerable<GetUIEventDto> SavedUIEvents { get; set; }

        [Parameter]
        public List<EditEventItem> Changes { get; set; }

        [Parameter]
        public bool Editing { get; set; }

        protected override void OnParametersSet()
        {
            if (_previousEditing == Editing)
            {
                _shouldRender = false;
            }
            else
            {
                _shouldRender = true;
                _previousEditing = Editing;
            }
        }

        //protected override bool ShouldRender() => _shouldRender;
    }
}
