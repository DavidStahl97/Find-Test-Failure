using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUIPageCollection
    {
        void Add(Page page);

        void Remove(Page page);

        ValueTask<(IEnumerable<Page> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, string search);

        ValueTask<OneOf<(Page Page, int Count), NotFound>> FindUIElementPageAsync(int pageIndex, 
            int pageSize, int id, string search);

        ValueTask<OneOf<Page, NotFound>> GetByIdAsync(int id);

        ValueTask<IEnumerable<GetAllPageDto>> GetAllPages();
    }
}
