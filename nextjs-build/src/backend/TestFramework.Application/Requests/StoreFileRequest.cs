using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Requests
{
    public class StoreFileRequest
    {
        public byte[] Data { get; init; }

        public string FileName { get; init; }

        public long FileSize { get; init; }
    }
}
