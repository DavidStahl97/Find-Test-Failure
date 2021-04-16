using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Utils
{
    public class UriConverter : Converter<Uri>
    {
        public UriConverter()
        {
            SetFunc = uri => uri.ToString();
            GetFunc = s => new Uri(s);
        }
    }
}
