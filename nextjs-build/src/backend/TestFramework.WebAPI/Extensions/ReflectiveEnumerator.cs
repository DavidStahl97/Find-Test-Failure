using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestFramework.WebAPI.Extensions
{
    public static class ReflectiveEnumerator
    {
        public static IEnumerable<Type> GetSubClassTypes(Type abstractType)
        {
            return abstractType.Assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType && x.IsInheritedFrom(abstractType))
                .ToList();
        }

        public static bool IsInheritedFrom(this Type type, Type Lookup)
        {
            var baseType = type.BaseType;
            if (baseType == null)
                return false;

            if (baseType.IsGenericType
                    && baseType.GetGenericTypeDefinition() == Lookup)
                return true;

            return baseType.IsInheritedFrom(Lookup);
        }
    }
}
