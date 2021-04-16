using AutoMapper;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application.Handler.UITests.UITestCases
{
    public class GetDetailsHandler : AbstractHandler<int, GetUITestCaseDetailsDto, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetDetailsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async override Task<Response<GetUITestCaseDetailsDto, Failure<NotFound, ErrorCodes>>> ExecuteAsync(int id)
        {
            var result = await _repository.UITestCases.GetByIdWithEventsAsync(id);
            return await result.Match(
                testCase => CreateDtoWithUIElements(testCase),
                notFound => ReturnFailureAsync(notFound));
        }

        public async Task<Response<GetUITestCaseDetailsDto, Failure<NotFound, ErrorCodes>>> CreateDtoWithUIElements(
            UITestCase testCase)
        {
            await AddUIElementsAsync(testCase);
            await AddUserFiles(testCase);

            return _mapper.Map<GetUITestCaseDetailsDto>(testCase);
        }

        public async Task AddUIElementsAsync(UITestCase testCase)
        {
            var uiElementEvents = testCase.Events
                .Where(x => x is UIElementEvent)
                .Select(x => x as UIElementEvent)
                .ToList();

            var elementsIds = uiElementEvents
                .Select(x => x.UIElementId)
                .Distinct()
                .ToList();

            var uiElements = await _repository.UIElements.GetRangeByIdWithPageAsync(elementsIds);

            uiElementEvents.ForEach(x => x.UIElement = 
                uiElements.Single(element => element.Id == x.UIElementId));
        }

        public async Task AddUserFiles(UITestCase testCase)
        {
            var importFileEvents = testCase.Events
                .Where(x => x is ImportFileEvent)
                .Select(x => x as ImportFileEvent)
                .ToList();

            var importFileEventIds = importFileEvents
                .Select(x => x.UserFileId)
                .Distinct()
                .ToList();

            var userFiles = await _repository.UserFiles.GetRangeAsync(importFileEventIds);

            importFileEvents.ForEach(x => x.UserFile =
                userFiles.Single(file => file.Id == x.UserFileId));
        }
    }
}
