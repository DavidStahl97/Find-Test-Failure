using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application;

namespace TestFramework.Infrastructure.FileStorage
{
    class FileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;

        public FileStorage(IWebHostEnvironment env)
        {
            _env = env;
            Folder = Path.Combine(_env.ContentRootPath, "data", "user-files");
        }

        public async Task Store(string fileName, byte[] data)
        {
            var filePath = GetFilePath(fileName);
            await File.WriteAllBytesAsync(filePath, data);
        }

        public void Delete(string fileName)
        {
            var filePath = GetFilePath(fileName);
            File.Delete(filePath);
        }

        private string GetFilePath(string fileName)
        {
            if (Directory.Exists(Folder) == false)
            {
                Directory.CreateDirectory(Folder);
            }

            return Path.Combine(Folder, fileName);
        }

        public string Folder { get; }
    }
}
