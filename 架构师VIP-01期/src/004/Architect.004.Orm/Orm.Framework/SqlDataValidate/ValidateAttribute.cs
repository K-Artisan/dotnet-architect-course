using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Framework.SqlDataValidate
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }
}
