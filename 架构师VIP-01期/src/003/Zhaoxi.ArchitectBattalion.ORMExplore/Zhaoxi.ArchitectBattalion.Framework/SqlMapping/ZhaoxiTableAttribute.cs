using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework.SqlMapping
{
    /// <summary>
    /// 做表名称的别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ZhaoxiTableAttribute : ZhaoxiAbstractMappingAttribute
    {
        //private string _Name = null;
        public ZhaoxiTableAttribute(string tableName) : base(tableName)
        {
            //this._Name = tableName;
        }

        //public string GetName()
        //{
        //    return this._Name;
        //}
    }
}
