using AutoMapper;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.BackgroundTasks;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application.Handler.UITests.UITestRun
{
    public class PostHandler : AbstractHandler<PostUITestRunDto, Created, ErrorCodes>
    {
        private readonly IUITestStarter _starter;
        private readonly IRepository _repository;

        public PostHandler(IUITestStarter starter, IRepository repository)
        {
            _starter = starter;
            _repository = repository;
        }

        public override async Task<Response<Created, ErrorCodes>> ExecuteAsync(PostUITestRunDto request)
        {
            var testCases = await _repository.UITestCases.GetByIdsWithEvents(request.SelectedTestCases);
            if (testCases.Count() != request.SelectedTestCases.Count())
            {
                return new ErrorCodes(ErrorCode.UITestRuns_TestCasesNotFound);
            }

            var runId = await _starter.ExecuteAsync(request.SelectedTestCases);

            return new Created { Id = runId };
        }
    }
}
