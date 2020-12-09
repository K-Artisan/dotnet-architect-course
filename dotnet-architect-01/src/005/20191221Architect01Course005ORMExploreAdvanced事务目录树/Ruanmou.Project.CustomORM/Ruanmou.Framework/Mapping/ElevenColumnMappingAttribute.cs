using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ElevenColumnMappingAttribute : AbstractMappingAttribute
    {
        public ElevenColumnMappingAttribute(string name) : base(name)
        {
        }

    }
}
