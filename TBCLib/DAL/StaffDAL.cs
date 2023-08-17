using MySqlConnector;
using Model;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    public class StaffDAL
    {
        private MySqlConnection connection = DBConfig.GetConnection();
        private string query = "";
        public Staff GetStaffAccount(string UserName)
        {
            Staff staff = new Staff();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();

                }
                string query = @"select * from staffs where Staff_Name = @Staff_Name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Staff_Name", UserName);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    staff = GetStaff(reader);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return staff;
        }

        public Staff GetStaff(MySqlDataReader reader)
        {
            Staff staff = new Staff();
            staff.StaffId = reader.GetInt32("Staff_id");
            staff.StaffName = reader.GetString("Staff_name");
            staff.Password = reader.GetString("Password");
            return staff;
        }
        public string CreateMD5(string input)
        {

            // Creates an instance of the default implementation of the MD5 hash algorithm.
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(input);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }
    }
}