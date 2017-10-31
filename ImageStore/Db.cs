using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore
{
    public class Db
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private const string ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename='E:\\Visual Studio 2017\\Projects\\YAShop\\ImageStore\\App_Data\\ImageStore.mdf';Integrated Security = True";

        public static SqlConnection Open()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
