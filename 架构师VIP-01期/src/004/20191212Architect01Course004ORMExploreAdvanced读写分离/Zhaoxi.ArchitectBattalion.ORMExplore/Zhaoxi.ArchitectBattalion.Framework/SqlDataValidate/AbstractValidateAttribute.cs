using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework.SqlDataValidate
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AbstractValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }
}
