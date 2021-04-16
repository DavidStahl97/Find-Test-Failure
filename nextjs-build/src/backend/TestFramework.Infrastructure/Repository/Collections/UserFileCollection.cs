using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository.Collections;
using TestFramework.Domain;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UserFileCollection : CollectionBase<UserFile>, IUserFileCollection
    {
        public UserFileCollection(RepositoryContext context) : base(context)
        {
        }

        public void Add(UserFile userFile) => AddEntity(userFile);

        public void Remove(UserFile userFile) => RemoveEntity(userFile);

        public ValueTask<OneOf<UserFile, NotFound>> GetByIdAsync(int id)
            => GetEntityByIdAsync(id);

        public ValueTask<(IEnumerable<UserFile> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, string search)
            => FindEntityPageAscendingOrderAsync(pageIndex, pageSize,
                    x => x.FileName, search: x => x.FileName.Contains(search));

        public Task<int> GetRangeCountByIdAsync(IEnumerable<int> ids)
            => Context.UserFiles.Where(x => ids.Contains(x.Id))
                .CountAsync();

        public async Task<IEnumerable<UserFile>> GetRangeAsync(IEnumerable<int> ids)
            => await Context.UserFiles.Where(x => ids.Contains(x.Id))
                    .ToListAsync();
    }
}
