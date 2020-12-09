
using Ruanmou.Framework;

namespace Ruanmou.Libraries.Model
{
    /// <summary>
    /// 数据库basemodel
    /// </summary>
    public class BaseModel
    {
        [ElevenKeyAttribute]
        public int Id { set; get; }
    }
}
