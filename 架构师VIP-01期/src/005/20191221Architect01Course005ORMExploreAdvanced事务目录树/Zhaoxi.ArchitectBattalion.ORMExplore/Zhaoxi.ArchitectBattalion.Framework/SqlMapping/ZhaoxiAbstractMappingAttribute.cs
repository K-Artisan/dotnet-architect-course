using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework.SqlMapping
{
    public abstract class ZhaoxiAbstractMappingAttribute : Attribute
    {
        private string _Name = null;
        public ZhaoxiAbstractMappingAttribute(string name)
        {
            this._Name = name;
        }

        public string GetName()
        {
            return this._Name;
        }
    }
}
