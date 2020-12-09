using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework
{
    public static class KeyAttributeFilter
    {
        public static IEnumerable<PropertyInfo> NotKey(this IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Where(p => !p.IsDefined(typeof(ElevenKeyAttribute), true));
        }
    }
}
