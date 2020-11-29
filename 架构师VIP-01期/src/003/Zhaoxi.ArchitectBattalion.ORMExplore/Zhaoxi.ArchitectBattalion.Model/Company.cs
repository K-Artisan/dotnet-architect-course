using System;
using Zhaoxi.ArchitectBattalion.Framework.SqlMapping;

namespace Zhaoxi.ArchitectBattalion.Model
{
    /// <summary>
    /// ���ݿ���Company  ���ǳ�����CompanyModel
    /// </summary>
    //[ZhaoxiTable("Company;Company")]
    [ZhaoxiTable("Company")]
    public class CompanyModel : BaseModel
    {
        [ZhaoxiColumn("Name")]
        public string CompanyName { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public Nullable<int> LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }
    }
}