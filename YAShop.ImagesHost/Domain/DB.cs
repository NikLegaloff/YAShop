using System.Data.SqlClient;
using Dapper;

namespace YAShop.ImagesHost.Domain
{
    public class DB
    {
        public static SqlConnection Open()
        {
            var connection = new SqlConnection(Env.Current.ConnectionString);
            connection.Open();
            return connection;
        }

        public static void Execute(string sql, object param = null)
        {
            using (var db = Open())
            {
                db.Execute(sql, param);
            }
        }

        public static  T ExecuteScalar<T>(string sql, object param = null)
        {
            using (var db = Open())
            {
                return  db.ExecuteScalar<T>(sql, param);
            }
        }
    }
}