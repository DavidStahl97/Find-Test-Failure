using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;
using TestFramework.Contract;

namespace TestFramework.Client.Pages.UIElements
{
    public partial class UIElementDialog : ComponentBase
    {
        private DirectValidationForm _form;
        private bool _isValidForm = true;

        [Parameter]
        public ChangeOrCreateUIElemenDto UIElement { get; set; }

        [Parameter]
        public string EventName { get; set; }

        [Parameter]
        public Func<ChangeOrCreateUIElemenDto, Task> Func { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        private Task ClickEvent()
        {
            return _form.ExecuteForm(async () =>
            {
                await Func(UIElement);
                MudDialog.Close(DialogResult.Ok(true));
            });
        }

        private readonly FluentValueValidator<string> _nameValidation = new(x => x
            .NotEmpty()
            .MaximumLength(Contracts.UIElements.NameMaxLength));

        private readonly FluentValueValidator<string> _findByValidation = new(x => x
            .NotEmpty());
    }
}
