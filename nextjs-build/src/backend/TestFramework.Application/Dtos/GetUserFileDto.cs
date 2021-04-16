using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos
{
    public class GetUserFileDto
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
