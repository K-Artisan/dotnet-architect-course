using Ruanmou.DAL.ExpressionExtend;
using Ruanmou.ExpressionDemo.SqlExtend;
using Ruanmou.Framework;
using Ruanmou.Libraries.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.DAL
{
    public class SqlHelper
    {
        private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;

        public T Find<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string tableName = type.GetMappingName();
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
            string sql = $@"SELECT {columnString} FROM [{tableName}] WHERE ID = @Id";
            SqlParameter[] sqlParameterList = new SqlParameter[] {
                new SqlParameter("@Id",id)
            };
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(sqlParameterList);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = Activator.CreateInstance<T>();
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(t, reader[prop.GetMappingName()] is DBNull ? null : reader[prop.GetMappingName()]);
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
            Type type = typeof(T);
           
            string sql = SqlBuilder<T>.GetSql();

            var sqlParameterList = type.GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(sqlParameterList);
                conn.Open();
                int iResult = command.ExecuteNonQuery();
                return iResult == 1;
            }
        }
    }
}




