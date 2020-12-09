using Ruanmou.Framework;
using Ruanmou.Libraries.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.DAL
{
    public class SqlBuilder<T> where T : BaseModel
    {
        private static string InsertSql = null;

        static SqlBuilder()
        {
            {
                Type type = typeof(T);
                string columnString = string.Join(",", type.GetProperties().NotKey().Select(p => $"[{p.Name}]"));
                string valueString = string.Join(",", type.GetProperties().NotKey().Select(p => $"@{p.Name}"));
                InsertSql = $"Insert Into [{type.Name}] ({columnString}) Values ({valueString})";
            }
        }

        public static string GetSql()
        {
            return InsertSql;
        }

    }
}
