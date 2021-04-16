using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUserFileCollection
    {
        void Add(UserFile userFile);

        void Remove(UserFile userFile);

        ValueTask<(IEnumerable<UserFile> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, string search);

        ValueTask<OneOf<UserFile, NotFound>> GetByIdAsync(int id);

        Task<int> GetRangeCountByIdAsync(IEnumerable<int> ids);

        Task<IEnumerable<UserFile>> GetRangeAsync(IEnumerable<int> ids);
    }
}
