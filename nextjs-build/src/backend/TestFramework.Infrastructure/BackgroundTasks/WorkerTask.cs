using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application;

namespace TestFramework.Infrastructure.BackgroundTasks
{
    public class WorkerTask<TMessage>
    {
        private Task _task = Task.CompletedTask;
        private readonly IBackgroundWorker<TMessage> _worker;

        public WorkerTask(IBackgroundWorker<TMessage> worker)
        {
            _worker = worker;
        }

        public bool IsBusy => _task.IsCompleted == false;

        public Task Task => _task;

        public void Execute(TMessage message, CancellationToken token)
        {
            if (IsBusy)
            {
                throw new InvalidOperationException("The Worker is busy");
            }

            _task = _worker.ExecuteTaskAsync(message, token);
        }
    }
}
