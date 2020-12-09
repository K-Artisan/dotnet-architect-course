using Zhaoxi.ArchitectBattalion.Framework.SqlFilter;

namespace Zhaoxi.ArchitectBattalion.Model
{
    /// <summary>
    /// 数据库BaseModel
    /// </summary>
    public class BaseModel
    {
        [ZhaoxiKey]
        public int Id { set; get; }
    }
}
