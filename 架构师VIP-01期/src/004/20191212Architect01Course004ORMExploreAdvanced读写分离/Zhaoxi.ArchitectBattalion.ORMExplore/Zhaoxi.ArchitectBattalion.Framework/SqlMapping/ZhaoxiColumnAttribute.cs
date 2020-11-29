using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework.SqlMapping
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ZhaoxiColumnAttribute : ZhaoxiAbstractMappingAttribute
    {
        //private string _Name = null;
        public ZhaoxiColumnAttribute(string columnName):base(columnName)
        {
            //this._Name = columnName;
        }

        //public string GetName()
        //{
        //    return this._Name;
        //}
    }
}
