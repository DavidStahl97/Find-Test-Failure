using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestFramework.Application.BackgroundTasks
{
    public interface IBackgroundTaskQueue<TMessage>
    {
        void EnqueueBackgroundTask(TMessage message);

        Task<(TMessage message, CancellationToken token)> DequeueAsync(CancellationToken cancellationToken);
    }
}
