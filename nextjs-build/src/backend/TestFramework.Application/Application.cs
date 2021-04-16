using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application
{
    public class Application
    {
        public static Assembly GetAssembly() => typeof(Application).Assembly;
    }
}
