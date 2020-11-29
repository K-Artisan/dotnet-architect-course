using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework
{
    public abstract class AbstractMappingAttribute : Attribute
    {
        private string _MappingName = null;
        public AbstractMappingAttribute(string name)
        {
            this._MappingName = name;
        }
        public string GetMappingName()
        {
            return this._MappingName;
        }

    }
}
