using MySqlConnector;

namespace DAL
{
    public class DBConfig
    {
        private static MySqlConnection connection = new MySqlConnection();
        private DBConfig() { }
        public static MySqlConnection GetDefaultConnection()
        {
            return GetConnection("server=cian;user id=root;password=ciandide;port=3306;database=OrderDB;IgnoreCommandTransaction=true;");
        }

        public static MySqlConnection GetConnection()
        {
            try
            {
                string conString;
                using (System.IO.FileStream fileStream = System.IO.File.OpenRead("DbConfig.txt"))
                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    conString = reader.ReadLine() ?? "server=localhost;user id=vtca;password=vtcacademy;port=3306;database=OrderDB;IgnoreCommandTransaction=true;";
                }

                if (!conString.Contains("IgnoreCommandTransaction=true"))
                {
                    conString += "IgnoreCommandTransaction=true;";
                }
                return GetConnection(conString);
            }
            catch
            {
                return GetDefaultConnection();
            }
        }

        public static MySqlConnection GetConnection(string connectionString)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            return connection;
        }
    }
}