using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Types;

namespace TestFramework.Application.Pipeline
{
    public class Response<T1, Fail> : OneOfBase<T1, Fail>
    {
        public Response(Fail fail)
            : base(fail)
        {
        }

        public Response(T1 t1)
            : base(t1)
        {
        }

        protected Response(OneOf<T1, Fail> input)
            : base(input)
        {
        }

        public static implicit operator Response<T1, Fail>(T1 _) => new(_);
        public static implicit operator Response<T1, Fail>(Fail _) => new(_);
    }
}
