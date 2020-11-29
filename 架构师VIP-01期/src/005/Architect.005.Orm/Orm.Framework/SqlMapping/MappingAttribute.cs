using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Framework.SqlMapping
{
    public class MappingAttribute : Attribute
    {
        private string _Name = null;
        public MappingAttribute(string name)
        {
            this._Name = name;
        }

        public string GetName()
        {
            return this._Name;
        }
    }
}
