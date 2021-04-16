using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.Pages.TestCases.EditEvents;
using TestFramework.Client.WebAPI;
using TestFramework.Utils;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class TestCase : ComponentBase
    {
        private GetUITestCaseDetailsDto _testCase;
        private List<EditEventItem> _changes;
        private bool _editing = false;

        private DirectValidationForm _validationForm;

        [Parameter]
        public int TestCaseId { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        protected override Task OnInitializedAsync() => UpdateData();

        private async Task UpdateData()
        {
            _testCase = await Api.SendAsync(x => x.GetUITestDetailsAsync(TestCaseId));
            _changes = _testCase.Events
                .OrderBy(x => x.Step)
                .Select(x => CreateEditEvent(x))
                .WithIndex()
                .Select(x => new EditEventItem 
                { 
                    EditEvent = x.item,
                    Index = x.index
                })
                .ToList();
        }

        private void Edit()
        {
            _editing = true;
        }

        private Task CancelEditing()
        {
            _editing = false;
            return UpdateData();
        }

        private async Task SaveChanges()
        {
            await _validationForm.ExecuteForm(async () =>
            {
                var updatedEvents = _changes.Select(x =>
                {
                    var updated = CreatePutEvent(x.EditEvent);
                    updated.Step = x.Index;
                    return updated;
                })
                .ToList();

                var dto = new UpdateEventsDto
                {
                    Events = updatedEvents
                };

                await Api.SendAsync(x => x.PutUITestCaseEventsAsync(_testCase.Id, dto));

                await UpdateData();

                _editing = false;
            });
        }

        private static EditEvent CreateEditEvent(GetUIEventDto uiEvent)
            => uiEvent switch
            {
                GetClickEventDto click => click.Read(),
                GetWriteEventDto write => write.Read(),
                GetWaitEventDto wait => wait.Read(),
                GetMoveToUIElementEventDto moveTo => moveTo.Read(),
                GetClickAtPositionEventDto clickAt => clickAt.Read(),
                GetMoveByOffsetEventDto moveByOffset => moveByOffset.Read(),
                GetClearContentEventDto clear => clear.Read(),
                GetImportFileEventDto import => import.Read(),
                _ => throw new ArgumentException("unhandled event type")
            };

        private static PutUIEventDto CreatePutEvent(EditEvent x)
        {
            return x switch
            {
                EditClickEvent click => click.Write(),
                EditWriteEvent write => write.Write(),
                EditWaitEvent wait => wait.Write(),
                EditMoveToUIElementEvent moveTo => moveTo.Write(),
                EditClickAtPositionEvent clickAt => clickAt.Write(),
                EditMoveByOffsetEvent moveByOffset => moveByOffset.Write(),
                EditClearContentEvent clear => clear.Write(),
                EditImportFileEvent import => import.Write(),
                _ => throw new ArgumentException("unhandled event type")
            };
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine("render page");
                base.OnAfterRender(firstRender);
        }
    }
}
