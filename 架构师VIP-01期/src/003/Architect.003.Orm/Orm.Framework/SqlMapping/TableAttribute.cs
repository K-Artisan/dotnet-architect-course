using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Framework.SqlMapping
{
    /// <summary>
    /// 做表名称的别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : MappingAttribute
    {
        public TableAttribute(string tableName) : base(tableName)
        {

        }
    }
}
