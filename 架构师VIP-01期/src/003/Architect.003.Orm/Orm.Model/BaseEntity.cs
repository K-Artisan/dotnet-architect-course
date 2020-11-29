using Orm.Framework.SqlFilter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Model
{
    /// <summary>
    /// BaseEntity
    /// </summary>
    public class BaseEntity
    {
        [Key]
        public int Id { set; get; }
    }
}
