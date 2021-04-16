using System.Collections.Generic;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.UITests
{
    public interface IRemoteWebDriverApi
    {
        Task<IEnumerable<EventLog>> GetLogsAsync(string sessionId);
    }
}