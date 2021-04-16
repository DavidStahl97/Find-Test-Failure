using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Types
{
    public class ErrorCodes : List<ErrorCode>
    {
        public ErrorCodes(ErrorCode errorCode)
            : base(new[] { errorCode })
        {
        }

        public ErrorCodes(IEnumerable<ErrorCode> errorCodes)
            : base(errorCodes)
        {
        }
    }
}
