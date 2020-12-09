using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ElevenTableMappingAttribute : AbstractMappingAttribute
    {
        public ElevenTableMappingAttribute(string name) : base(name)
        {
        }

    }
}
