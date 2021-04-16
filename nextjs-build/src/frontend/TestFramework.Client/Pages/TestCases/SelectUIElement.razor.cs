using FluentValidation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Pages.TestCases.EditEvents;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class SelectUIElement : ComponentBase
    {
        [Parameter]
        public SelectedUIElementData Selected { get; set; }

        [Parameter]
        public IEnumerable<GetAllPageDto> Pages { get; set; }

        private readonly FluentValueValidator<GetAllUIElementDto> _uiElementValidation = new(x => x
            .NotNull());
    }
}
