using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Orm.Framework.SqlDataValidate
{
    public static class ValidateExtend
    {
        public static bool Validate<T>(this T t)
        {
            Type type = t.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(ValidateAttribute), true))
                {
                    object oValue = prop.GetValue(t);
                    var attributeArray = prop.GetCustomAttributes<ValidateAttribute>();
                    foreach (var attribute in attributeArray)
                    {
                        if (attribute.Validate(oValue))
                        {
                            //继续
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
