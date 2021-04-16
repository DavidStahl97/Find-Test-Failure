using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application.BackgroundTasks;

namespace TestFramework.Infrastructure.BackgroundTasks
{
    // https://docs.microsoft.com/de-de/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio
    public class BackgroundTaskQueue<TMessage> : IBackgroundTaskQueue<TMessage>
    {
        private readonly ConcurrentQueue<TMessage> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public void EnqueueBackgroundTask(TMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _queue.Enqueue(message);
            _signal.Release();
        }

        public async Task<(TMessage message, CancellationToken token)> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _queue.TryDequeue(out var message);

            return (message, cancellationToken);
        }
    }
}
