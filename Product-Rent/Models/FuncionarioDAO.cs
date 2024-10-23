using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class FuncionarioDAO
    {
        private static ConnectionMysql conn;

        public FuncionarioDAO() 
        {
            conn = new ConnectionMysql();
        }
    }
}