using Orm.Orm.Framework;
using System;
using System.Data.SqlClient;
using System.Linq;
using Orm.Framework.SqlMapping;
using Orm.Model;

namespace Orm.DAL
{
    public class SqlHelper
    {
        public T Find<T>(int id) where T : BaseEntity
        {
            Type type = typeof(T);

            /*   
             string columsString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
             string sql = $"select {columsString} from [{type.GetTableName()}] where id={id}";
            */

            string sql = $"{SqlBuilder<T>.GetFindSql()}{id}";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.SqlConnectionString))
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

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.SqlConnectionString))
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

        public bool Insert<T>(T entity) where T: BaseEntity
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

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paraArray);
                conn.Open();
                int iResult = command.ExecuteNonQuery();
                return iResult == 1;
            }
        }
    }

}
