using System;
using System.Data.SqlClient;
using System.Linq;
using Zhaoxi.ArchitectBattalion.Framework;
using Zhaoxi.ArchitectBattalion.Framework.SqlFilter;
using Zhaoxi.ArchitectBattalion.Framework.SqlMapping;
using Zhaoxi.ArchitectBattalion.Model;
using Zhaoxi.ArchitectBattalion.Framework.SqlDataValidate;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zhaoxi.ArchitectBattalion.DAL.ExpressionExtend;

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
        public T Find<T>(int id) where T : BaseModel, new()
        {
            Type type = typeof(T);
            //string columnsString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
            //string sql = $"SELECT {columnsString} FROM [{type.GetMappingName()}] WHERE ID={id} ";
            string sql = $"{SqlBuilder<T>.GetFindSql()}{id}";
            string connString = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Read);
            Console.WriteLine($"当前查询的字符串为{connString}");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = new T();
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

        public IList<T> FindCondition<T>(Expression<Func<T, bool>> func) where T : BaseModel, new()
        {
            Type type = typeof(T);
            string columnsString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
            string where = func.ToWhere<T>(out List<SqlParameter> parameters);
            string sql = $"SELECT {columnsString} FROM [{type.GetMappingName()}] WHERE {where}";
            string connString = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Read);
            Console.WriteLine($"当前查询的字符串为{connString}");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters.ToArray());
                conn.Open();
                var reader = command.ExecuteReader();
                IList<T> result = new List<T>();
                while (reader.Read())
                {
                    T t = new T();
                    foreach (var prop in type.GetProperties())
                    {
                        string propName = prop.GetMappingName();
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);
                    }
                    result.Add(t);
                }
                return result;
            }
        }


        public int Insert<T>(T t) where T : BaseModel, new()
        {
            Type type = t.GetType();
            //string columnsString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]"));
            //string valuesString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"@{p.GetMappingName()}"));
            //string sql = $"INSERT INTO [{type.GetMappingName()}] ({columnsString}) VALUES({valuesString});";//不能直接拼装值---Sql注入问题
            string sql = SqlBuilder<T>.GetInsertSql();
            var paraArray = type.GetProperties().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            string connString = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                object oId = command.ExecuteScalar();

                return int.TryParse(oId?.ToString(), out int iId) ? iId : -1;
            }

        }

        public int Update<T>(T t) where T : BaseModel, new()
        {
            if (!t.Validate<T>())
            {
                throw new Exception("数据校验没有通过");//大家可以再返回点提示信息
            }

            Type type = t.GetType();
            string stringSet = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"{p.GetMappingName()}=@{p.Name}"));
            string sql = $"UPDATE [{type.GetMappingName()}] SET {stringSet} WHERE Id=@Id;";
            //string sql = SqlBuilder<T>.GetInsertSql();
            var paraArray = type.GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            string connString = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 怎么样用表达式目录树？！
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="paraArray">属性-值  属性-值</param>
        public void Update<T>(Expression<Func<T, bool>> func, params object[] paraArray) where T : BaseModel, new()
        {

        }

        //按需更新--界面上只修改了几个字段--大家有什么思路，可以自己动手，然后show出来
        //1 传递一个列表---实体三个属性  字段名称--操作符--值
        //2 改造成json---{"Name":"zzzz","Password":"123245"}
        public int Update<T>(string json, int id) where T : BaseModel, new()
        {
            Type type = typeof(T);
            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);//JObject
            string stringSet = string.Join(",", type.GetPropertiesInJson(json).Select(p => $"{p.GetMappingName()}=@{p.Name}"));
            string sql = $"UPDATE {type.GetMappingName()} SET {stringSet} WHERE Id=@Id;";

            var paraArray = type.GetPropertiesInJson(json).Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t) ?? DBNull.Value)).Append(new SqlParameter("@Id", id)).ToArray();

            string connString = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }

        public bool Delete<T>(T t) where T : BaseModel, new()
        {
            Type type = t.GetType();
            string sql = $"DELETE FROM [{type.GetMappingName()}] WHERE Id=@Id;";
            var paraArray = new SqlParameter[] { new SqlParameter("@Id", t.Id) };

            string connString = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                //var trans = conn.BeginTransaction();
                //trans.Commit();
                //trans.Rollback();
                return 1 == command.ExecuteNonQuery();
            }
        }



        //Context--->延迟提交 Add Remove 保存命令--->一次性生成语句
    }
}

