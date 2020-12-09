using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework.SqlMapping
{
    public static class MappingExtend
    {
        //简化

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">可以是type  也可以是property</param>
        /// <returns></returns>
        public static string GetMappingName(this MemberInfo type)
        {
            if (type.IsDefined(typeof(ZhaoxiAbstractMappingAttribute), true))
            {
                var attribute = type.GetCustomAttribute<ZhaoxiAbstractMappingAttribute>();
                return attribute.GetName();
            }
            else
            {
                return type.Name;
            }
        }


        public static string GetTableName(this Type type)
        {
            if (type.IsDefined(typeof(ZhaoxiTableAttribute), true))
            {
                ZhaoxiTableAttribute attribute = type.GetCustomAttribute<ZhaoxiTableAttribute>();
                return attribute.GetName();
            }
            else
            {
                return type.Name;
            }
        }
        public static string GetColumnName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ZhaoxiColumnAttribute), true))
            {
                ZhaoxiColumnAttribute attribute = prop.GetCustomAttribute<ZhaoxiColumnAttribute>();
                return attribute.GetName();
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
