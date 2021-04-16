using AutoMapper;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application.Handler.UITests.UITestCases
{
    public class UpdateEventsHandler : 
        AbstractHandler<UpdateEventsRequest, Success, Failure<ErrorCodes, NotFound>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UpdateEventsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<Success, Failure<ErrorCodes, NotFound>>> ExecuteAsync(UpdateEventsRequest request)
        {
            var events = await _repository.UITestCases.GetEventsAsync(request.Id);
            return await events.Match(
                events => UpdateEventsAsync(request, events),
                notFound => ReturnFailureAsync(notFound));            
        }

        public async Task<Response<Success, Failure<ErrorCodes, NotFound>>> UpdateEventsAsync(UpdateEventsRequest request,
            IEnumerable<UIEvent> oldEvents)
        {
            if (request.Dto.Events is null || request.Dto.Events.Any() == false)
            {
                await UpdateEvents(oldEvents, new List<UIEvent>());
                return new Success();
            }

            var newEvents = _mapper.Map<List<UIEvent>>(request.Dto.Events);
            newEvents.ForEach(x => x.UITestCaseId = request.Id);

            if (await AreUIElementsExisting(newEvents) == false)
            {
                return ReturnFailure(new ErrorCodes(ErrorCode.UITestCases_UIElementNotFound));
            }

            if (await AreUserFilesExisting(newEvents) == false)
            {
                return ReturnFailure(new ErrorCodes(ErrorCode.UITestCases_UserFilesNotFound));
            }

            await UpdateEvents(oldEvents, newEvents);

            return new Success();
        }

        private async Task<bool> AreUIElementsExisting(IEnumerable<UIEvent> newEvents)
        {
            var distinctElementIds = newEvents
                .Where(x => x is UIElementEvent)
                .Select(x => x as UIElementEvent)
                .Select(x => x.UIElementId)
                .Distinct()
                .ToList();

            var uiElementCount = await _repository.UIElements.GetRangeCountByIdAsync(distinctElementIds);
            return uiElementCount == distinctElementIds.Count;
        }

        private async Task<bool> AreUserFilesExisting(IEnumerable<UIEvent> newEvents)
        {
            var distinctUserFiles = newEvents
                .Where(x => x is ImportFileEvent)
                .Select(x => x as ImportFileEvent)
                .Select(x => x.UserFileId)
                .Distinct()
                .ToList();

            var filesCount = await _repository.UserFiles.GetRangeCountByIdAsync(distinctUserFiles);
            return filesCount == distinctUserFiles.Count;
        }

        private async Task UpdateEvents(
            IEnumerable<UIEvent> oldEvents, IEnumerable<UIEvent> newEvents)
        {
            _repository.UIEvents.RemoveRange(oldEvents);
            _repository.UIEvents.AddRange(newEvents);
            await _repository.SaveChangesAsync();
        }
    }
}
