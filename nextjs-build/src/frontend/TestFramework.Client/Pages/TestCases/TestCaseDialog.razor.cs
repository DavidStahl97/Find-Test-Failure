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

namespace TestFramework.Client.Pages.TestCases
{
    public partial class TestCaseDialog : ComponentBase
    {
        private DirectValidationForm _form;
        private bool _isValidForm = true;

        [Parameter]
        public ChangeOrCreateUITestCaseDto TestCase { get; set; } = new();

        [Parameter]
        public string EventName { get; set; }

        [Parameter]
        public Func<ChangeOrCreateUITestCaseDto, Task> Func { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        private Task ClickEvent()
        {
            return _form.ExecuteForm(async () =>
            {
                await Func(TestCase);
                MudDialog.Close(DialogResult.Ok(true));
            });
        }

        private readonly FluentValueValidator<string> _nameValidator = new(x => x
            .NotEmpty()
            .MaximumLength(Contracts.UITestCases.NameMaxLength));

        private readonly FluentValueValidator<Uri> _uriValidator = new(x => x
            .NotNull());
    }
}
