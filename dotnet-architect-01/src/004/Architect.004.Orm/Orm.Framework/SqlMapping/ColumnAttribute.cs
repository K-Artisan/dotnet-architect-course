using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Framework.SqlMapping
{
    public class ColumnAttribute : MappingAttribute
    {
        public ColumnAttribute(string columnName) : base(columnName)
        {

        }
    }
}
