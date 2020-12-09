using Orm.Framework.SqlMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Model
{
    /// <summary>
    /// 数据库中表名Company  但是程序是CompanyModel
    /// </summary>
    [Table("Company")]
    public class CompanyModel : BaseEntity
    {
        [Column("Name")]
        public string CompanyName { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public Nullable<int> LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }
    }
}
