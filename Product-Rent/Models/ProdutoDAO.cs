using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;
using System;
using System.Collections.Generic;

namespace Product_Rent.Models
{
    public class ProdutoDAO
    {
        private static ConnectionMysql conn;

        public ProdutoDAO()
        {
            conn = new ConnectionMysql();
        }

        public int Insert(ProdutoDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_Produto (@nome, @marca, @tamanho, @cor, @valor_aluguel, @descricao, @id_for)";

                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@marca", item.Marca);
                query.Parameters.AddWithValue("@tamanho", item.Tamanho);
                query.Parameters.AddWithValue("@cor", item.Cor);
                query.Parameters.AddWithValue("@valor_aluguel", item.Valor_aluguel);
                query.Parameters.AddWithValue("@descricao", item.Descricao);
                query.Parameters.AddWithValue("@id_for", item.IdFornecedor); // Utilizando o FornecedorId

                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar inserir o produto.");
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Produto> GetAll()
        {
            try
            {
                List<Produto> list = new List<Produto>();
                var query = conn.Query();
                query.CommandText = "CALL select_produto();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Produto()
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Marca = reader.GetString("marca"),
                        Tamanho = reader.GetString("tamanho"),
                        Cor = reader.GetString("cor"),
                        Valor_aluguel = reader.GetDouble("valor_aluguel"), // Ajustado o nome da coluna
                        Descricao = reader.GetString("descricao"),
                        IdFornecedor = new Fornecedor()
                        {
                            Id = reader.GetInt32("id"),
                            NomeFantasia = reader.GetString("nome")
                        }
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar os produtos.");
            }
            finally
            {
                conn.Close();
            }
        }

        public Produto GetById(int id)
        {
            try
            {
                Produto produto = null;
                var query = conn.Query();
                query.CommandText = "CALL select_produto_id(@id);";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    produto = new Produto()
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Marca = reader.GetString("marca"),
                        Tamanho = reader.GetString("tamanho"),
                        Cor = reader.GetString("cor"),
                        Valor_aluguel = reader.GetDouble("valor_aluguel"), // Ajustado o nome da coluna
                        Descricao = reader.GetString("descricao"),
                        IdFornecedor = new Fornecedor()
                        {
                            Id = reader.GetInt32("id"),
                            NomeFantasia = reader.GetString("nome")
                        }
                    };
                }
                return produto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar o produto.");
            }
            finally
            {
                conn.Close();
            }
        }

        public Produto Update(int id, ProdutoDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL update_produto(@id, @nome, @marca, @tamanho, @cor, @valor_aluguel, @descricao, @id_for_fk);";

                query.Parameters.AddWithValue("@id", id);
                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@marca", item.Marca);
                query.Parameters.AddWithValue("@tamanho", item.Tamanho);
                query.Parameters.AddWithValue("@cor", item.Cor);
                query.Parameters.AddWithValue("@valor_aluguel", item.Valor_aluguel);
                query.Parameters.AddWithValue("@descricao", item.Descricao);
                query.Parameters.AddWithValue("@id_for_fk", item.IdFornecedor);

                var result = query.ExecuteNonQuery();

                if (result < 0)
                {
                    throw new Exception("Ocorreu um erro ao atualizar o Produto.");
                }
                return GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o Produto.");
            }
            finally
            {
                conn.Close();
            }
        }

        public Produto Inative(int id) // Ajustado para retornar void
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL inative_produto(@id);";
                query.Parameters.AddWithValue("@id", id);
                query.ExecuteNonQuery();

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar desativar o produto.");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
