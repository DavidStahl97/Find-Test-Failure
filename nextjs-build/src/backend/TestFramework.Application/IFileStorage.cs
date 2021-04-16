using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application
{
    public interface IFileStorage
    {
        Task Store(string fileName, byte[] data);

        void Delete(string fileName);

        string Folder { get; }
    }
}
