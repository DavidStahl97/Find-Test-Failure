using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Application.Repository.Collections;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UIPageCollection : CollectionBase<Page>, IUIPageCollection
    {
        public UIPageCollection(RepositoryContext context) : base(context)
        {
        }

        public void Add(Page page) => AddEntity(page);

        public ValueTask<(IEnumerable<Page> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, string search)
            => FindEntityPageAscendingOrderAsync(pageIndex, pageSize, x => x.Name,
                search: x => x.Name.Contains(search));

        public async ValueTask<OneOf<(Page Page, int Count), NotFound>> FindUIElementPageAsync(int pageIndex, 
            int pageSize, int id, string search)
        {
            var page = await Context.Page
                .AddTracking(false)
                .Include(x => x.UIElements
                    .Where(e => e.Name.Contains(search))
                    .OrderBy(e => e.Name)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize))
                .SingleOrDefaultAsync(x => x.Id == id);

            if (page is null)
            {
                return new NotFound();
            }

            var count = await Context.UIElements
                .Where(x => x.PageId == id)
                .CountAsync();

            return (page, count);
        }

        public ValueTask<OneOf<Page, NotFound>> GetByIdAsync(int id)
            => GetEntityByIdAsync(id);

        public void Remove(Page page) => RemoveEntity(page);

        public async ValueTask<IEnumerable<GetAllPageDto>> GetAllPages()
            => await Context.Page
                .Include(x => x.UIElements)
                .Select(x => new GetAllPageDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UIElements = x.UIElements.Select(u => new GetAllUIElementDto
                    {
                        Id = u.Id,
                        Name = u.Name
                    })
                })
                .ToListAsync();
    }
}
