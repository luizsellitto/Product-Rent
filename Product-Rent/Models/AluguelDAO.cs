using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class AluguelDAO
    {
        private static ConnectionMysql conn;

        public AluguelDAO()
        {
            conn = new ConnectionMysql();
        }
        public int Insert(AluguelDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_aluguel (@data_retirada, @data_devolucao, @valor_total, @id_fun_fk, @id_cli_fk)";

                query.Parameters.AddWithValue("@data_retirada", item.DataRetirada);
                query.Parameters.AddWithValue("@data_devolucao", item.DataDevolucao);
                query.Parameters.AddWithValue("@valor_total", item.ValorTotal);
                query.Parameters.AddWithValue("@id_fun_fk", item.IdFuncionario);
                query.Parameters.AddWithValue("@id_cli_fk", item.IdCliente);

                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar cadastrar um aluguel.");
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Aluguel> GetAll()
        {
            try
            {
                List<Aluguel> list = new List<Aluguel>();
                var query = conn.Query();
                query.CommandText = "CALL select_aluguel();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Aluguel()
                    {
                        Id = reader.GetInt32("id"),
                        DataDevolucao = reader.GetDateTime("data_devolucao"),
                        DataRetirada = reader.GetDateTime("data_retirada"),
                        ValorTotal = reader.GetDouble("valor_total"),
                        IdFuncionario = reader.GetInt32("id_fun_fk"),       
                        IdCliente = reader.GetInt32("id_cli_fk")
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar os alugueis.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Aluguel GetById(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL select_aluguel_id(@id);";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();
                Aluguel aluguel = null;
                if (reader.Read())
                {
                    aluguel = new Aluguel()
                    {
                        Id = reader.GetInt32("id"),
                        DataDevolucao = reader.GetDateTime("data_devolucao"),
                        DataRetirada = reader.GetDateTime("data_retirada"),
                        ValorTotal = reader.GetDouble("valor_total"),
                        IdFuncionario = reader.GetInt32("id_fun_fk"),
                        IdCliente = reader.GetInt32("id_cli_fk")
                    };
                }
                return aluguel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar o aluguel.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Aluguel Update(int id, AluguelDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL update_fornecedor (@id, @data_retirada, @data_devolucao, @valor_total, @id_fun_fk, @id_cli_fk)";

                query.Parameters.AddWithValue("@id", id);
                query.Parameters.AddWithValue("@data_retirada", item.DataRetirada);
                query.Parameters.AddWithValue("@data_devolucao", item.DataDevolucao);
                query.Parameters.AddWithValue("@valor_total", item.ValorTotal);
                query.Parameters.AddWithValue("@id_fun_fk", item.IdFuncionario);
                query.Parameters.AddWithValue("@id_cli_fk", item.IdCliente);
                var result = query.ExecuteNonQuery();
                if (result < 0)
                {
                    throw new Exception("Ocorreu um erro ao atualizar o aluguel.");
                }
                return GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o aluguel.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Aluguel Inative(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL inative_aluguel(@id);";
                query.Parameters.AddWithValue("@id", id);

                query.ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar desativar o aluguel.");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
