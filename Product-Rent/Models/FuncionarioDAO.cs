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

        public int Insert(Funcionario item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_funcionario (@nome, @data_nascimento, @sexo, @rg, @cpf, @telefone, @email, @ctps, @funcao, @cep, @rua, @numero, @bairro, @cidade, @estado, 'Funcionário'); SELECT LAST_INSERT_ID();";

                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@data_nascimento", item.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss"));
                query.Parameters.AddWithValue("@sexo", item.Sexo);
                query.Parameters.AddWithValue("@rg", item.Rg);
                query.Parameters.AddWithValue("@cpf", item.Cpf);
                query.Parameters.AddWithValue("@telefone", item.Telefone);
                query.Parameters.AddWithValue("@email", item.Email);
                query.Parameters.AddWithValue("@ctps", item.Ctps);
                query.Parameters.AddWithValue("@funcao", item.Funcao);
                query.Parameters.AddWithValue("@cep", item.Endereco.CEP);
                query.Parameters.AddWithValue("@rua", item.Endereco.Rua);
                query.Parameters.AddWithValue("@numero", item.Endereco.Numero);
                query.Parameters.AddWithValue("@bairro", item.Endereco.Bairro);
                query.Parameters.AddWithValue("@cidade", item.Endereco.Cidade);
                query.Parameters.AddWithValue("@estado", item.Endereco.Estado);
                query.Parameters.AddWithValue("Funcionário", item.Endereco.Tipo);


                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}