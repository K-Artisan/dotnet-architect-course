using System;
using System.Data.SqlClient;
using System.Linq;
using Zhaoxi.ArchitectBattalion.Framework;
using Zhaoxi.ArchitectBattalion.Framework.SqlFilter;
using Zhaoxi.ArchitectBattalion.Framework.SqlMapping;
using Zhaoxi.ArchitectBattalion.Model;

namespace Zhaoxi.ArchitectBattalion.DAL
{
    /// <summary>
    /// 数据库查询帮助类库--自动生成Sql--通用
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 通用主键查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            //string columnsString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
            //string sql = $"SELECT {columnsString} FROM [{type.GetMappingName()}] WHERE ID={id} ";

            string sql = $"{SqlBuilder<T>.GetFindSql()}{id}";

            using (SqlConnection conn = new SqlConnection(ConfigrationManager.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        string propName = prop.GetMappingName();//查询时as一下，可以省下一轮
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);//可空类型  设置成null而不是数据库查询的值
                    }
                    return t;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public bool Insert<T>(T t) where T : BaseModel
        {
            Type type = t.GetType();
            //string columnsString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]"));
            //string valuesString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"@{p.GetMappingName()}"));
            //string sql = $"INSERT INTO [{type.GetMappingName()}] ({columnsString}) VALUES({valuesString});";//不能直接拼装值---Sql注入问题
            string sql = SqlBuilder<T>.GetInsertSql();
            var paraArray = type.GetProperties().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            using (SqlConnection conn = new SqlConnection(ConfigrationManager.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                int iResult = command.ExecuteNonQuery();
                return iResult == 1;
            }

        }
        //Context--->延迟提交 Add Remove 保存命令--->一次性生成语句
    }
}

