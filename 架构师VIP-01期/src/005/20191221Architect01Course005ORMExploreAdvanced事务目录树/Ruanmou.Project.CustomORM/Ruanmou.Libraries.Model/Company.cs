using Ruanmou.Framework;
using System;

namespace Ruanmou.Libraries.Model
{
    [ElevenTableMappingAttribute("Company")]
    public class Company : BaseModel
    {
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public Nullable<int> LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }
    }
}