using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework.SqlFilter
{
    public static class FilterExtend
    {
        /// <summary>
        /// 过滤掉主键 返回全部属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithoutKey(this Type type)
        {
            return type.GetProperties().Where(p => !p.IsDefined(typeof(ZhaoxiKeyAttribute), true));
        }
    }
}
