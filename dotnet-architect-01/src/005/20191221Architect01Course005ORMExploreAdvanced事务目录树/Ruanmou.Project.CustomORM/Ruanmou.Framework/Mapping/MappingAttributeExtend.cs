using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace Ruanmou.Framework
{
    public static class MappingAttributeExtend
    {
        public static string GetMappingName<T>(this T type) where T : MemberInfo
        {
            if (type.IsDefined(typeof(AbstractMappingAttribute), true))
            {
                AbstractMappingAttribute attribute = type.GetCustomAttribute<AbstractMappingAttribute>();
                return attribute.GetMappingName();
            }
            else
            {
                return type.Name;
            }
        }

        //public static string GetTableName(this Type type)
        //{
        //    if (type.IsDefined(typeof(ElevenTableMappingAttribute), true))
        //    {
        //        ElevenTableMappingAttribute attribute = type.GetCustomAttribute<ElevenTableMappingAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return type.Name;
        //    }
        //}

        //public static string GetColumnName(this PropertyInfo prop)
        //{
        //    if (prop.IsDefined(typeof(ElevenColumnMappingAttribute), true))
        //    {
        //        ElevenColumnMappingAttribute attribute = prop.GetCustomAttribute<ElevenColumnMappingAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return prop.Name;
        //    }
        //}
    }
}
