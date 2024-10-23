using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using Product_Rent.DataBase;

namespace Product_Rent.Models
{
    public class FornecedorDAO
    {
        private static ConnectionMysql conn;

        public FornecedorDAO()
        {
            conn = new ConnectionMysql();
        }
        public int Insert(Fornecedor item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_fornecedor (@razao_social, @nome_fantasia, @cnpj, @inscricao_estadual, @inscricao_municipal, @responsavel, @contato_1, @contato_2, @contato_3, @email_1, @email_2, @cep, @rua, @numero, @bairro, @cidade, @estado, 'Fornecedor'); SELECT LAST_INSERT_ID();";

                query.Parameters.AddWithValue("@razao_social", item.RazaoSocial);
                query.Parameters.AddWithValue("@nome_fantasia", item.NomeFantasia);
                query.Parameters.AddWithValue("@cnpj", item.CNPJ);
                query.Parameters.AddWithValue("@inscricao_estadual", item.InscricaoEstadual);
                query.Parameters.AddWithValue("@inscricao_municipal", item.InscricaoMunicipal);
                query.Parameters.AddWithValue("@responsavel", item.Responsavel);
                query.Parameters.AddWithValue("@contato_1", item.ContatoUm);
                query.Parameters.AddWithValue("@contato_2", item.ContatoDois);
                query.Parameters.AddWithValue("@contato_3", item.ContatoTres);
                query.Parameters.AddWithValue("@email_1", item.EmailUm);
                query.Parameters.AddWithValue("@email_2", item.EmailDois);
                query.Parameters.AddWithValue("@cep", item.Endereco.CEP);
                query.Parameters.AddWithValue("@rua", item.Endereco.Rua);
                query.Parameters.AddWithValue("@numero", item.Endereco.Numero);
                query.Parameters.AddWithValue("@bairro", item.Endereco.Bairro);
                query.Parameters.AddWithValue("@cidade", item.Endereco.Cidade);
                query.Parameters.AddWithValue("@estado", item.Endereco.Estado);
                query.Parameters.AddWithValue("Fornecedor", item.Endereco.Tipo);

                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar inserir o fornecedor.");
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Fornecedor> GetAll()
        {
            try
            {
                List<Fornecedor> list = new List<Fornecedor>();
                var query = conn.Query();
                query.CommandText = "CALL select_fornecedor();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Fornecedor()
                    {
                        Id = reader.GetInt32("id"),
                        RazaoSocial = reader.GetString("razao_social"),
                        NomeFantasia = reader.GetString("nome_fantasia"),
                        CNPJ = reader.GetString("cnpj"),
                        InscricaoEstadual = reader.GetString("inscricao_estadual"),
                        InscricaoMunicipal = reader.GetString("inscricao_municipal"),
                        Responsavel = reader.GetString("responsavel"),
                        ContatoUm = reader.GetString("contato_1"),
                        ContatoDois = reader.GetString("contato_2"),
                        ContatoTres = reader.GetString("contato_3"),
                        EmailUm = reader.GetString("email_1"),
                        EmailDois = reader.GetString("email_2"),
                        Endereco = new Endereco()
                        {
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
            catch (Exception ex) 
            { 
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar os fornecedores.");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
