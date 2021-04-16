using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Types
{
    public static class Types
    {
        public static OneOf<TEntity, NotFound> CreateNotFound<TEntity>(TEntity entity)
            => entity is null ? new NotFound() : entity;
    }
}
