using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class ClienteDAO
    {

        private static ConnectionMysql conn;

        public ClienteDAO()
        {
            conn = new ConnectionMysql();
        }

        public int Insert(ClienteDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_cliente (@nome, @data_nascimento, @sexo, @rg, @cnpj, @cpf, @telefone, @email, @cep, @rua, @numero, @bairro, @cidade, @estado, 'Cliente');";//Tinha um  SELECT LAST_INSERT_ID();

                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@data_nascimento", item.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss"));
                query.Parameters.AddWithValue("@sexo", item.Sexo);
                query.Parameters.AddWithValue("@rg", item.RG);
                query.Parameters.AddWithValue("@cnpj", item.CNPJ);
                query.Parameters.AddWithValue("@cpf", item.CPF);
                query.Parameters.AddWithValue("@telefone", item.Telefone);
                query.Parameters.AddWithValue("@email", item.Email);
                query.Parameters.AddWithValue("@cep", item.Endereco.CEP);
                query.Parameters.AddWithValue("@rua", item.Endereco.Rua);
                query.Parameters.AddWithValue("@numero", item.Endereco.Numero);
                query.Parameters.AddWithValue("@bairro", item.Endereco.Bairro);
                query.Parameters.AddWithValue("@cidade", item.Endereco.Cidade);
                query.Parameters.AddWithValue("@estado", item.Endereco.Estado);
                query.Parameters.AddWithValue("Cliente", item.Endereco.Tipo);


                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar inserir o cliente.");
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Cliente> GetAll()
        {
            try
            {
                List<Cliente> list = new List<Cliente>();
                var query = conn.Query();
                query.CommandText = "CALL select_cliente();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Cliente()
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        CNPJ = reader.GetString("cnpj"),
                        CPF = reader.GetString("cpf"),
                        RG = reader.GetString("rg"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email"),
                        DataNascimento = reader.GetDateTime("data_nascimento"),
                        Sexo = reader.GetString("sexo"),
                        Endereco = new Endereco()
                        {
                            CEP = reader.GetString("cep"),
                            Rua = reader.GetString("rua"),
                            Numero = reader.GetInt32("numero"),
                            Bairro = reader.GetString("bairro"),
                            Cidade = reader.GetString("cidade"),
                            Estado = reader.GetString("estado"),
                        },
                        Status = reader.GetBoolean("status")
                    });

                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar os clientes.");
            }
            finally
            {
                conn.Close();
            }
        }

        public Cliente GetById(int id)
        {
            try
            {
                Cliente cliente = null;
                var query = conn.Query();
                query.CommandText = "CALL select_cliente_id(@id);";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {

                    cliente = new Cliente()
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        CPF = reader.GetString("cpf"),
                        CNPJ = reader.GetString("cnpj"),
                        RG = reader.GetString("rg"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email"),
                        DataNascimento = reader.GetDateTime("data_nascimento"),
                        Sexo = reader.GetString("sexo"),
                        Endereco = new Endereco()
                        {
                            CEP = reader.GetString("cep"),
                            Rua = reader.GetString("rua"),
                            Numero = reader.GetInt32("numero"),
                            Bairro = reader.GetString("bairro"),
                            Cidade = reader.GetString("cidade"),
                            Estado = reader.GetString("estado"),
                        },
                        Status = reader.GetBoolean("status")
                    };
                }
                return cliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar o cliente.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Cliente Update(int id, ClienteDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL update_cliente(@id, @nome, @data_nascimento, @sexo, @rg, @cnpj, @cpf, @telefone, @email, @cep, @rua, @numero, @bairro, @cidade, @estado); ";

                query.Parameters.AddWithValue("@id", id);
                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@data_nascimento", item.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss"));
                query.Parameters.AddWithValue("@sexo", item.Sexo);
                query.Parameters.AddWithValue("@rg", item.RG);
                query.Parameters.AddWithValue("cnpj", item.CNPJ);
                query.Parameters.AddWithValue("@cpf", item.CPF);
                query.Parameters.AddWithValue("@telefone", item.Telefone);
                query.Parameters.AddWithValue("@email", item.Email);
                //endereço
                query.Parameters.AddWithValue("@cep", item.Endereco.CEP);
                query.Parameters.AddWithValue("@rua", item.Endereco.Rua);
                query.Parameters.AddWithValue("@numero", item.Endereco.Numero);
                query.Parameters.AddWithValue("@bairro", item.Endereco.Bairro);
                query.Parameters.AddWithValue("@cidade", item.Endereco.Cidade);
                query.Parameters.AddWithValue("@estado", item.Endereco.Estado);

                var result = query.ExecuteNonQuery();

                if (result < 0)
                {
                    throw new Exception("Ocorreu um erro ao atualizar o cliente.");
                }
                return GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o cliente.");
            }
            finally
            {
                conn.Close();
            }
        }
        public void Inative(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL inative_cliente(@id);";
                query.Parameters.AddWithValue("@id", id);
                query.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar desativar o cliente.");
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
