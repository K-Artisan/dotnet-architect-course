using Orm.Orm.Framework;
using System;
using System.Data.SqlClient;
using System.Linq;
using Orm.Framework.SqlMapping;
using Orm.Model;
using Orm.Framework;
using static Orm.Framework.SqlConnectionPool;
using Orm.Framework.SqlFilter;
using Orm.Framework.SqlDataValidate;

namespace Orm.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlHelperOther
    {
        public T Find<T>(int id) where T : BaseEntity
        {
            Type type = typeof(T);

            /*   
             string columsString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
             string sql = $"select {columsString} from [{type.GetTableName()}] where id={id}";
            */

            string sql = $"{SqlBuilder<T>.GetFindSql()}{id}";
            string connectString = GetOtherDbConnectionString(SqlConnectionType.Read);
            Console.WriteLine($"当前查询的字符串为{connectString}");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = (T)Activator.CreateInstance<T>();
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(t, reader[prop.GetColumnName()]);
                    }

                    return t;
                }
            }

            return default(T);
        }


        /// <summary>
        /// 查询部分字段
        /// </summary>
        /// <typeparam name="E">数据库映射Model类</typeparam>
        /// <typeparam name="D">Dto类型</typeparam>
        /// <param name="id">id</param>
        /// <returns>Dto类型</returns>
        public D Find<E,D>(int id) where E : BaseEntity
        {
            Type modleType = typeof(E);
            Type dtoType = typeof(D);

            string columsString = string.Join(",", dtoType.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"select {columsString} from [{modleType.GetTableName()}] where id={id}";

            string connectString = GetOtherDbConnectionString(SqlConnectionType.Read);
            Console.WriteLine($"当前查询的字符串为{connectString}");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    D dto = (D)Activator.CreateInstance<D>();
                    foreach (var prop in dtoType.GetProperties())
                    {
                        prop.SetValue(dto, reader[prop.GetColumnName()]);
                    }

                    return dto;
                }
            }

            return default(D);
        }

        public int Insert<T>(T entity) where T: BaseEntity
        {
            Type type = typeof(T);
         /*
            string columnsString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]"));
            string valuesString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"@{p.GetMappingName()}"));
            var insertSql = $"INSERT INTO [{type.GetMappingName()}] ({columnsString}) VALUES({valuesString});";
         */
            string sql = SqlBuilder<T>.GetInsertSql();
            var paraArray = type.GetProperties().Select(
                   p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(entity) ?? DBNull.Value)
               ) .ToArray();

            string connectString = GetOtherDbConnectionString(SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                object id = command.ExecuteScalar();
                return Convert.ToInt32(id);
            }
        }

        public int Update<T>(T t) where T : BaseEntity, new()
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

            string connString = SqlConnectionPool.GetOtherDbConnectionString(SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }

        //按需更新--界面上只修改了几个字段--大家有什么思路，可以自己动手，然后show出来
        //1 传递一个列表---实体三个属性  字段名称--操作符--值
        //2 改造成json---{"Name":"zzzz","Password":"123245"}
        public int Update<T>(string json, int id) where T : BaseEntity, new()
        {
            Type type = typeof(T);
            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);//JObject
            string stringSet = string.Join(",", type.GetPropertiesInJson(json).Select(p => $"{p.GetMappingName()}=@{p.Name}"));
            string sql = $"UPDATE {type.GetMappingName()} SET {stringSet} WHERE Id=@Id;";

            var paraArray = type.GetPropertiesInJson(json).Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t) ?? DBNull.Value)).Append(new SqlParameter("@Id", id)).ToArray();

            string connString = SqlConnectionPool.GetOtherDbConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }

        public bool Delete<T>(T t) where T : BaseEntity, new()
        {
            Type type = t.GetType();
            string sql = $"DELETE FROM [{type.GetMappingName()}] WHERE Id=@Id;";
            var paraArray = new SqlParameter[] { new SqlParameter("@Id", t.Id) };

            string connString = SqlConnectionPool.GetOtherDbConnectionString(SqlConnectionPool.SqlConnectionType.Write);
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

    }

}
