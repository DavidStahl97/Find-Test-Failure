using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Application.Repository;
using TestFramework.Application.Repository.Collections;
using TestFramework.Infrastructure.Repository.Collections;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly RepositoryContext _context;
        private readonly ILogger<Repository> _logger;

        public Repository(RepositoryContext context, ILogger<Repository> logger)
        {
            _context = context;
            _logger = logger;

            UIElements = new UIElementCollection(_context);
            UITestCases = new UITestCaseCollection(_context);
            UIEvents = new UIEventsCollection(_context);
            UITestRuns = new UITestRunCollection(_context);
            UITestRunUIElements = new UITestRunUIElementCollection(_context);
            UITestRunEvents = new UITestRunEventCollection(_context);
            UITestRunCases = new UITestRunCaseCollection(_context);
            UIPages = new UIPageCollection(_context);
            HealthChecks = new HealthCheckCollection(_context);
            UserFiles = new UserFileCollection(_context);
        }

        public IUIElementCollection UIElements { get; }

        public IUITestCaseCollection UITestCases { get; }

        public IUIEventCollection UIEvents { get; }

        public IUITestRunCollection UITestRuns { get; }

        public IUITestRunUIElementCollection UITestRunUIElements { get; }

        public IUITestRunEventCollection UITestRunEvents { get; }

        public IUITestRunCaseCollection UITestRunCases { get; }

        public IUIPageCollection UIPages { get; }

        public IHealthCheckCollection HealthChecks { get; }

        public IUserFileCollection UserFiles { get; }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();

        public async Task<TrueOrFalse> TryDeleteEntityAsync<TEntity>(TEntity entity)
        {
            try
            {
                _context.Attach(entity);
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.WriteError(ex, "Try Delete Entity failed. Maybe because entity does not exists");
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.WriteError(ex, "Try Delete Entity failed. Maybe because of foreign keys");
                return false;
            }
        }
    }
}
