using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.HealthChecks;
using TestFramework.Application.Dtos.UITests;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Dtos.UITests.UITestRunCases;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Domain;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // UIElements
            CreateMap<UIElement, GetUIElementDto>();
            CreateMap<ChangeOrCreateUIElemenDto, UIElement>();

            // UITestCases
            CreateMap<ChangeOrCreateUITestCaseDto, UITestCase>();
            CreateMap<UITestCase, GetUITestCaseDto>();
            CreateMap<UITestCase, GetUITestCaseDetailsDto>();

            // UIEvents
            CreateMap<PutUIEventDto, UIEvent>()
                .Include<PutClickEventDto, ClickEvent>()
                .Include<PutWaitEventDto, WaitEvent>()
                .Include<PutWriteEventDto, WriteEvent>()
                .Include<PutMoveToUIElementEventDto, MoveToUIElementEvent>()
                .Include<PutClickAtPositionEventDto, ClickAtPositionEvent>()
                .Include<PutMoveByOffsetEventDto, MoveByOffsetEvent>()
                .Include<PutClearContentEventDto, ClearContentEvent>()
                .Include<PutImportFileEventDto, ImportFileEvent>();

            CreateMap<PutClickEventDto, ClickEvent>();
            CreateMap<PutWaitEventDto, WaitEvent>();
            CreateMap<PutWriteEventDto, WriteEvent>();
            CreateMap<PutMoveToUIElementEventDto, MoveToUIElementEvent>();
            CreateMap<PutClickAtPositionEventDto, ClickAtPositionEvent>();
            CreateMap<PutMoveByOffsetEventDto, MoveByOffsetEvent>();
            CreateMap<PutClearContentEventDto, ClearContentEvent>();
            CreateMap<PutImportFileEventDto, ImportFileEvent>();

            CreateMap<UIEvent, GetUIEventDto>()
                .Include<ClickEvent, GetClickEventDto>()
                .Include<WaitEvent, GetWaitEventDto>()
                .Include<WriteEvent, GetWriteEventDto>()
                .Include<MoveToUIElementEvent, GetMoveToUIElementEventDto>()
                .Include<ClickAtPositionEvent, GetClickAtPositionEventDto>()
                .Include<MoveByOffsetEvent, GetMoveByOffsetEventDto>()
                .Include<ClearContentEvent, GetClearContentEventDto>()
                .Include<ImportFileEvent, GetImportFileEventDto>();

            CreateMap<ClickEvent, GetClickEventDto>();
            CreateMap<WaitEvent, GetWaitEventDto>();
            CreateMap<WriteEvent, GetWriteEventDto>();
            CreateMap<MoveToUIElementEvent, GetMoveToUIElementEventDto>();
            CreateMap<ClickAtPositionEvent, GetClickAtPositionEventDto>();
            CreateMap<MoveByOffsetEvent, GetMoveByOffsetEventDto>();
            CreateMap<ClearContentEvent, GetClearContentEventDto>();
            CreateMap<ImportFileEvent, GetImportFileEventDto>();

            CreateMap<UIElement, GetUIElementFromEvent>();

            // UITestRuns
            CreateMap<UITestCase, UITestRunCase>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.AutomaticallyStarted, opt => opt.MapFrom(y => y.RunsPeriodically));

            CreateMap<UIEvent, UITestRunEvent>()
                .Include<ClickEvent, UITestRunClickEvent>()
                .Include<WaitEvent, UITestRunWaitEvent>()
                .Include<WriteEvent, UITestRunWriteEvent>()
                .Include<MoveToUIElementEvent, UITestRunMoveToUIElementEvent>()
                .Include<ClickAtPositionEvent, UITestRunClickAtPositionEvent>()
                .Include<MoveByOffsetEvent, UITestRunMoveByOffsetEvent>()
                .Include<ClearContentEvent, UITestRunClearContentEvent>()
                .Include<ImportFileEvent, UITestRunImportFileEvent>();

            CreateMap<ClickEvent, UITestRunClickEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UIElement, opt => opt.Ignore())
                .ForMember(x => x.UITestRunUIElementId, opt => opt.MapFrom(y => y.UIElementId));

            CreateMap<WaitEvent, UITestRunWaitEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<WriteEvent, UITestRunWriteEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UIElement, opt => opt.Ignore())
                .ForMember(x => x.UITestRunUIElementId, opt => opt.MapFrom(y => y.UIElementId));

            CreateMap<MoveToUIElementEvent, UITestRunMoveToUIElementEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UIElement, opt => opt.Ignore())
                .ForMember(x => x.UITestRunUIElementId, opt => opt.MapFrom(y => y.UIElementId));

            CreateMap<ImportFileEvent, UITestRunImportFileEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UIElement, opt => opt.Ignore())
                .ForMember(x => x.UITestRunUIElementId, opt => opt.MapFrom(y => y.UIElementId))
                .ForMember(x => x.FileName, opt => opt.MapFrom(y => y.UserFile.FileName))
                .ForMember(x => x.StoredFileName, opt => opt.MapFrom(y => y.UserFile.StoredFileName));

            CreateMap<ClickAtPositionEvent, UITestRunClickAtPositionEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<MoveByOffsetEvent, UITestRunMoveByOffsetEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<ClearContentEvent, UITestRunClearContentEvent>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UIElement, opt => opt.Ignore())
                .ForMember(x => x.UITestRunUIElementId, opt => opt.MapFrom(y => y.UIElementId));

            CreateMap<UIElement, UITestRunUIElement>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ClickEvents, opt => opt.Ignore())
                .ForMember(x => x.WriteEvents, opt => opt.Ignore())
                .ForMember(x => x.MoveToUIElements, opt => opt.Ignore())
                .ForMember(x => x.ClearContentEvents, opt => opt.Ignore())
                .ForMember(x => x.ImportFileEvents, opt => opt.Ignore());

            CreateMap<UITestRun, GetUITestRunCasesDto>();
            CreateMap<UITestRunCase, GetUITestRunCaseDto>();

            // UITestRunCases
            CreateMap<UITestRunCase, GetUITestRunCaseDetailsDto>();

            CreateMap<UITestRunEvent, GetUITestRunEventDto>()
                .Include<UITestRunClickEvent, GetUITestRunClickEventDto>()
                .Include<UITestRunWaitEvent, GetUITestRunWaitEventDto>()
                .Include<UITestRunWriteEvent, GetUITestRunWriteEventDto>()
                .Include<UITestRunMoveToUIElementEvent, GetUITestRunMoveToUIElementEventDto>()
                .Include<UITestRunClickAtPositionEvent, GetUITestRunClickAtPositionDto>()
                .Include<UITestRunMoveByOffsetEvent, GetUITestRunMoveByOffsetEventDto>()
                .Include<UITestRunClearContentEvent, GetUITestRunClearContentEventDto>()
                .Include<UITestRunImportFileEvent, GetUITestRunImportFileEventDto>();

            CreateMap<UITestRunClickEvent, GetUITestRunClickEventDto>();
            CreateMap<UITestRunWaitEvent, GetUITestRunWaitEventDto>();
            CreateMap<UITestRunWriteEvent, GetUITestRunWriteEventDto>();
            CreateMap<UITestRunMoveToUIElementEvent, GetUITestRunMoveToUIElementEventDto>();
            CreateMap<UITestRunClickAtPositionEvent, GetUITestRunClickAtPositionDto>();
            CreateMap<UITestRunMoveByOffsetEvent, GetUITestRunMoveByOffsetEventDto>();
            CreateMap<UITestRunClearContentEvent, GetUITestRunClearContentEventDto>();
            CreateMap<UITestRunImportFileEvent, GetUITestRunImportFileEventDto>();

            CreateMap<UITestRunUIElement, GetUITestRunUIElementDto>();

            CreateMap<EventLog, EventLogDto>();

            // UIPages
            CreateMap<ChangeOrCreateUIPageDto, Page>();
            CreateMap<Page, GetUIPageDto>();
            CreateMap<Page, GetUIPageDetailsDto>();

            // HealthChecks
            CreateMap<ChangeOrCreateHealthCheckDto, HealthCheck>();
            CreateMap<HealthCheck, GetHealthCheckDto>();

            // UserFiles
            CreateMap<UserFile, GetUserFileDto>();
        }
    }
}
