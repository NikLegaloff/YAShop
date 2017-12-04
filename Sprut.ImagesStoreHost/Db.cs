using System.Data.SqlClient;

namespace ImagesStoreHost
{
    public class Db
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private const string ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\ImageStore.mdf';Integrated Security = True";
        //private const string ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\VSProjects\\YAShop\\ImageStore\\App_Data\\ImageStore.mdf';Integrated Security = True";


        public static SqlConnection Open()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
