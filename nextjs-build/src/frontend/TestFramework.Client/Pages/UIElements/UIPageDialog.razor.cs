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
    public partial class UIPageDialog : ComponentBase
    {
        private bool _isValidForm = true;
        private DirectValidationForm _form;

        [Parameter]
        public ChangeOrCreateUIPageDto UIPage { get; set; } = new();

        [Parameter]
        public string EventName { get; set; }

        [Parameter]
        public Func<ChangeOrCreateUIPageDto, Task> Func { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        private Task ClickEvent()
        {
            return _form.ExecuteForm(async () =>
            {
                await Func(UIPage);
                MudDialog.Close(DialogResult.Ok(true));
            });
        }

        private readonly FluentValueValidator<string> _nameValidator = new(x => x
            .NotEmpty()
            .MaximumLength(Contracts.UIPages.NameMaxLength));
    }
}
