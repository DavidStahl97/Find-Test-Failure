using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests
{
    public abstract class TypeDiscriminatorDto : ITypeDiscriminator
    {
        public string TypeDiscriminator => CreatTypeDiscriminator();

        protected abstract string CreatTypeDiscriminator();
    }
}
