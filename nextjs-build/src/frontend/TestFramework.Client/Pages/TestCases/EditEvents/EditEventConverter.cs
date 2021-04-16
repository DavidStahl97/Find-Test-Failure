using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public static class EditEventConverter
    {
        private static T ReadUIEvent<T>(GetUIEventDto uiEvent, Func<T> create)
            where T : EditEvent
        {
            var edit = create();
            edit.Name = uiEvent.Name;
            return edit;
        }

        private static T WriteUIEvent<T>(EditEvent edit, Func<T> create)
            where T : PutUIEventDto
        {
            var uiEvent = create();
            uiEvent.Name = edit.Name;
            return uiEvent;
        }

        private static T ReadUIElementEvent<T>(GetUIElementEventDto uiEvent, Func<T> create)
            where T : EditUIElementEvent
            => ReadUIEvent(uiEvent, () =>
            {
                var edit = create();
                edit.WaitForUIElement = uiEvent.WaitForUIElement;
                edit.UseDefaultWaitForUIElement = uiEvent.UseDefaultWaitForUIElement;
                edit.SelectedUIElement = new SelectedUIElementData
                {
                    Page = new GetAllPageDto
                    {
                        Id = uiEvent.UIElement.Page.Id,
                        Name = uiEvent.UIElement.Page.Name,
                        UIElements = new List<GetAllUIElementDto>()
                    },
                    UIElement = new GetAllUIElementDto
                    {
                        Id = uiEvent.UIElement.Id,
                        Name = uiEvent.UIElement.Name
                    }
                };
                return edit;
            });

        private static T WriteUIElementEvent<T>(EditUIElementEvent edit, Func<T> create)
            where T : PutUIElementEventDto
            => WriteUIEvent(edit, () =>
            {
                var uiEvent = create();
                uiEvent.WaitForUIElement = edit.WaitForUIElement;
                uiEvent.UseDefaultWaitForUIElement = edit.UseDefaultWaitForUIElement;
                uiEvent.UIElementId = edit.SelectedUIElement.UIElement.Id;
                return uiEvent;
            });

        public static EditClickEvent Read(this GetClickEventDto clickEvent)
            => ReadUIElementEvent(clickEvent, () => new EditClickEvent());

        public static PutClickEventDto Write(this EditClickEvent edit)
            => WriteUIElementEvent(edit, () => new PutClickEventDto());

        public static EditWriteEvent Read(this GetWriteEventDto writeEvent)
            => ReadUIElementEvent(writeEvent, () => new EditWriteEvent
            {
                GenerateUnique = writeEvent.GenerateUnique,
                Input = writeEvent.Input
            });

        public static PutWriteEventDto Write(this EditWriteEvent edit)
            => WriteUIElementEvent(edit, () => new PutWriteEventDto
            {
                GenerateUnique = edit.GenerateUnique,
                Input = edit.Input
            });

        public static EditWaitEvent Read(this GetWaitEventDto waitEvent)
            => ReadUIEvent(waitEvent, () => new EditWaitEvent
            {
                Ticks = waitEvent.Ticks
            });

        public static PutWaitEventDto Write(this EditWaitEvent edit)
            => WriteUIEvent(edit, () => new PutWaitEventDto
            {
                Ticks = edit.Ticks
            });

        public static EditMoveToUIElementEvent Read(this GetMoveToUIElementEventDto moveTo)
            => ReadUIElementEvent(moveTo, () => new EditMoveToUIElementEvent());

        public static PutMoveToUIElementEventDto Write(this EditMoveToUIElementEvent edit)
            => WriteUIElementEvent(edit, () => new PutMoveToUIElementEventDto());

        public static EditClickAtPositionEvent Read(this GetClickAtPositionEventDto click)
            => ReadUIEvent(click, () => new EditClickAtPositionEvent());

        public static PutClickAtPositionEventDto Write(this EditClickAtPositionEvent edit)
            => WriteUIEvent(edit, () => new PutClickAtPositionEventDto());

        public static EditMoveByOffsetEvent Read(this GetMoveByOffsetEventDto moveByOffset)
            => ReadUIEvent(moveByOffset, () => new EditMoveByOffsetEvent
            {
                OffsetX = moveByOffset.OffsetX,
                OffsetY = moveByOffset.OffsetY
            });

        public static PutMoveByOffsetEventDto Write(this EditMoveByOffsetEvent edit)
            => WriteUIEvent(edit, () => new PutMoveByOffsetEventDto
            {
                OffsetX = edit.OffsetX,
                OffsetY = edit.OffsetY
            });

        public static EditClearContentEvent Read(this GetClearContentEventDto clear)
            => ReadUIElementEvent(clear, () => new EditClearContentEvent());

        public static PutClearContentEventDto Write(this EditClearContentEvent edit)
            => WriteUIElementEvent(edit, () => new PutClearContentEventDto());

        public static EditImportFileEvent Read(this GetImportFileEventDto import)
            => ReadUIElementEvent(import, () => new EditImportFileEvent
            {
                UserFile = import.UserFile
            });

        public static PutImportFileEventDto Write(this EditImportFileEvent edit)
            => WriteUIElementEvent(edit, () => new PutImportFileEventDto
            {
                UserFileId = edit.UserFile.Id
            });
    }
}
