using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Framework.SqlDataValidate
{
    public class RequiredAttribute : ValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            return oValue != null && !string.IsNullOrWhiteSpace(oValue.ToString());
        }
    }
}
