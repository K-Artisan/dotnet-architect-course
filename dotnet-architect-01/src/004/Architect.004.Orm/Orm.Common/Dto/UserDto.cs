using Orm.Framework.SqlMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Common.Dto
{
    public class UserDto
    {
        public int id { get; set; }

        [Column("Name")]
        public string UserName { get; set; }

    }
}
