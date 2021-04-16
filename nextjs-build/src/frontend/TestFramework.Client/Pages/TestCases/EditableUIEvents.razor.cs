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
using TestFramework.Utils;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class EditableUIEvents : ComponentBase
    {
        private IEnumerable<GetAllPageDto> _allPages = new List<GetAllPageDto>();
        private IEnumerable<GetUserFileDto> _allFiles = new List<GetUserFileDto>();

        [Parameter]
        public List<EditEventItem> Events { get; set; }
        
        [Inject]
        public ITestFrameworkApi Api { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _allPages = await Api.SendAsync(x => x.GetAllPagesAsync());
            if (Events is not null)
            {
                Events.Where(x => x.EditEvent is EditUIElementEvent)
                    .Select(x => x.EditEvent as EditUIElementEvent)
                    .ToList()
                    .ForEach(x => x.SelectedUIElement.Page.UIElements = 
                        _allPages.SingleOrDefault(y => y.Id == x.SelectedUIElement.Page.Id).UIElements ?? new List<GetAllUIElementDto>());
            }

            var userFileResult = await Api.SendAsync(x => x.GetUserFilePageAsync(0, 100, string.Empty));
            _allFiles = userFileResult.Data;
        }

        private void AddEvent(int i)
            => UpdateIndices(() => Events.Insert(i,  new EditEventItem 
            {
                EditEvent = new EditClickEvent 
                { 
                    SelectedUIElement = new SelectedUIElementData() 
                }
            }));

        private void DeleteEvent(int i)
            => UpdateIndices(() => Events.RemoveAt(i));

        private void MoveEventTop(int i)
            => UpdateIndices(() => Events.Swap(i, i - 1));

        private void MoveEventBottom(int i)
            => UpdateIndices(() => Events.Swap(i, i + 1));

        private void UpdateIndices(Action action)
        {
            action();
            foreach (var (editEvent, i) in Events.WithIndex())
            {
                editEvent.Index = i;
            }
        }
    }
}
