using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository.Collections;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UITestRunEventCollection : CollectionBase<UITestRunEvent>, IUITestRunEventCollection
    {
        public UITestRunEventCollection(RepositoryContext context) : base(context)
        {
        }

        public ValueTask<OneOf<UITestRunEvent, NotFound>> GetByIdAsync(int id)
            => GetEntityByIdAsync(id);
    }
}
