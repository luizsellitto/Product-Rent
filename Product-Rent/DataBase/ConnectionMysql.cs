using MySql.Data.MySqlClient;
using System.Data;

namespace Product_Rent.DataBase
{
    public class ConnectionMysql
    {
        private static readonly string host = "localhost";
        private static readonly string port = "3306";
        private static readonly string user = "root";
        private static readonly string password = "root";
        private static readonly string dbname = "Elegance_Rent";
        private static MySqlConnection connection;
        private static MySqlCommand command;

        public ConnectionMysql()
        {
            try
            {
                connection = new MySqlConnection($"server={host}; database={dbname};port={port};user={user};password={password}");
                connection.Open();
            }
            catch(Exception e) 
            {
                throw e;
            }
        }
        public MySqlCommand Query()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                return command;
            }
            catch(Exception e) 
            { 
                throw e; 
            }
        }
        public void Close()
        {
            connection.Close();
        }
    }
}