using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUITestRunEventCollection
    {
        ValueTask<OneOf<UITestRunEvent, NotFound>> GetByIdAsync(int id);
    }
}
