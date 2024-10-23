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
        public List<Funcionario> GetAll()
        {
            try
            {
                List<Funcionario> list = new List<Funcionario>();
                var query = conn.Query();
                query.CommandText = "CALL select_funcionario();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Funcionario()
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Cpf = reader.GetString("cpf"),
                        Rg = reader.GetString("rg"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email"),
                        DataNascimento = reader.GetDateTime("data_nascimento"),
                        Sexo = reader.GetString("sexo"),
                        Ctps = reader.GetString("ctps"),
                        Funcao = reader.GetString("funcao"),
                        Endereco = new Endereco(){ 
                            CEP = reader.GetString("cep"),
                            Rua = reader.GetString("rua"),
                            Numero = reader.GetInt32("numero"),
                            Bairro = reader.GetString("bairro"),
                            Cidade = reader.GetString("cidade"),
                            Estado = reader.GetString("estado"),
                        },
                        Status = reader.GetBoolean("ativo")
                    });

                }
                return list;
            }
            catch (Exception) 
            {
                throw ;
            }
            finally
            {
                conn.Close() ;
            }
        }
        public Funcionario GetById(int id)
        {
            try
            {
                Funcionario funcionario = null;
                var query = conn.Query();
                query.CommandText = "CALL select_funcionario_id(@id);";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    funcionario = new Funcionario()
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Cpf = reader.GetString("cpf"),
                        Rg = reader.GetString("rg"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email"),
                        DataNascimento = reader.GetDateTime("data_nascimento"),
                        Sexo = reader.GetString("sexo"),
                        Ctps = reader.GetString("ctps"),
                        Funcao = reader.GetString("funcao"),
                        Endereco = new Endereco()
                        {
                            CEP = reader.GetString("cep"),
                            Rua = reader.GetString("rua"),
                            Numero = reader.GetInt32("numero"),
                            Bairro = reader.GetString("bairro"),
                            Cidade = reader.GetString("cidade"),
                            Estado = reader.GetString("estado")
                        },
                        Status = reader.GetBoolean("ativo")
                    };
                }
                return funcionario;
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