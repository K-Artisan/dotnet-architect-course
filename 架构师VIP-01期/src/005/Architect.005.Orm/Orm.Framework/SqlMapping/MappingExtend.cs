using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Orm.Framework.SqlMapping
{
    public static class MappingExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member">MemberInfo 是 Type 和 PropertyInfo的父类</param>
        /// <returns></returns>
        public static string GetMappingName(this MemberInfo member)
        {
            if (member.IsDefined(typeof(MappingAttribute), true))
            {
                MappingAttribute attribute = member.GetCustomAttribute<MappingAttribute>();
                return attribute.GetName();
            }
            else
            {
                return member.Name;
            }
        }

        public static string GetTableName(this Type type)
        {
            if (type.IsDefined(typeof(TableAttribute), true))
            {
                TableAttribute attribute = type.GetCustomAttribute<TableAttribute>();
                return attribute.GetName();
            }
            else
            {
                return type.Name;
            }
        }


        public static string GetColumnName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ColumnAttribute), true))
            {
                ColumnAttribute attribute = prop.GetCustomAttribute<ColumnAttribute>();
                return attribute.GetName();
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
