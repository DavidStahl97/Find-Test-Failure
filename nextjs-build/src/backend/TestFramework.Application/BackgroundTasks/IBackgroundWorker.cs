using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestFramework.Application
{
    public interface IBackgroundWorker<TMessage>
    {
        Task ExecuteTaskAsync(TMessage message, CancellationToken token);
    }
}
