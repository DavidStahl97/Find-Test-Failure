using FluentValidation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Pages.TestCases.EditEvents;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;
using TestFramework.Contract;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class EditableEvent : ComponentBase
    {
        private IEnumerable<EditEvent> _possableEvents = new List<EditEvent>
        {
            new EditClickEvent { SelectedUIElement = new SelectedUIElementData() },
            new EditWriteEvent 
            { 
                SelectedUIElement = new SelectedUIElementData(),
                Input = string.Empty,
            },
            new EditMoveToUIElementEvent { SelectedUIElement = new SelectedUIElementData() },
            new EditWaitEvent(),
            new EditClickAtPositionEvent(),
            new EditMoveByOffsetEvent(),
            new EditClearContentEvent { SelectedUIElement = new SelectedUIElementData() },
            new EditImportFileEvent { SelectedUIElement = new SelectedUIElementData() },
        };

        [Parameter]
        public EditEventItem Item { get; set; }

        [Parameter]
        public IEnumerable<GetAllPageDto> AllPages { get; set; }

        [Parameter]
        public IEnumerable<GetUserFileDto> AllUserFiles { get; set; }

        [Parameter]
        public int EventCount { get; set; }

        [Parameter]
        public EventCallback<int> OnMoveEventTop { get; set; }

        [Parameter]
        public EventCallback<int> OnMoveEventBottom { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleteEvent { get; set; }

        private static readonly FluentValueValidator<string> _nameValidation = new(x => x
            .NotEmpty()
            .MaximumLength(Contracts.UIEvents.NameMaxLength));

        private static readonly FluentValueValidator<string> _inputValidation = new(x => x
            .MaximumLength(Contracts.UIEvents.InputMaxLength));

        private static readonly FluentValueValidator<GetUserFileDto> _userFileValidation = new(x => x
            .NotNull());
    }
}
