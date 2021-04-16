using AutoMapper;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Application.Handler.UITests.UITestRunCases
{
    public class GetDetailsHandler : AbstractHandler<int, GetUITestRunCaseDetailsDto, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetDetailsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<GetUITestRunCaseDetailsDto, Failure<NotFound, ErrorCodes>>> ExecuteAsync(int request)
        {
            var response = await _repository.UITestRunCases.GetByIdWithEventsAsync(request);
            return await response.Match(
                testCase => AddUIElementsAsync(testCase),
                notFound => ReturnFailureAsync(notFound));
        }

        public async Task<Response<GetUITestRunCaseDetailsDto, Failure<NotFound, ErrorCodes>>> AddUIElementsAsync(
            UITestRunCase testCase)
        {
            var elementIds = testCase.Events
                .Where(x => x is UITestRunUIElementEvent)
                .Select(x => x as UITestRunUIElementEvent)
                .Select(x => x.UITestRunUIElementId)
                .Distinct()
                .ToList();

            var uiElements = await _repository.UITestRunUIElements.GetRangeByIdsAsync(elementIds);

            testCase.Events
                .Where(x => x is UITestRunUIElementEvent)
                .Select(x => x as UITestRunUIElementEvent)
                .ToList()
                .ForEach(x => x.UIElement = uiElements.Single(y => y.Id == x.UITestRunUIElementId));

            return _mapper.Map<GetUITestRunCaseDetailsDto>(testCase);
        }
    }
}
