using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Pipeline
{
    public class Failure<T1, T2> : OneOfBase<T1, T2>
    {
        public Failure(T1 t1)
            : base(t1)
        {
        }

        public Failure(T2 t2)
            : base(t2)
        {
        }

        protected Failure(OneOf<T1, T2> input)
            : base(input)
        {
        }

        public static implicit operator Failure<T1, T2>(T1 _) => new Failure<T1, T2>(_);
        public static implicit operator Failure<T1, T2>(T2 _) => new Failure<T1, T2>(_);
    }
}
