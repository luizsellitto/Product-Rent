using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;
using System.Data;

namespace Product_Rent.Models
{
    public class PagamentoDAO
    {
        private static ConnectionMysql conn;

        public PagamentoDAO()
        {
            conn = new ConnectionMysql();
        }
        public int Insert(PagamentoDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_pagamento (@status, @valor, @parcela, @data, @forma, @id_cai, @id_des, @id_comp)";

                query.Parameters.AddWithValue("@status", item.Status);
                query.Parameters.AddWithValue("@valor", item.Valor);
                query.Parameters.AddWithValue("@parcela", item.Parcela);
                query.Parameters.AddWithValue("@data", item.Data);
                query.Parameters.AddWithValue("@forma", item.Forma);
                query.Parameters.AddWithValue("@id_cai", item.IdCaixa);
                query.Parameters.AddWithValue("@id_des", item.IdDespesa);
                query.Parameters.AddWithValue("@id_comp", item.IdCompra);

                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar cadastrar o pagamento.");
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Pagamento> GetAll()
        {
            try
            {
                List<Pagamento> list = new List<Pagamento>();
                var query = conn.Query();
                query.CommandText = "CALL select_pagamento();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Pagamento()
                    {
                        Id = reader.GetInt32("id"),
                        Status = reader.GetBoolean("status"),
                        Valor = reader.GetDouble("valor"),
                        Parcela = reader.GetInt32("parcela"),
                        Data = reader.GetDateTime("data"),
                        Forma = reader.GetString("forma"),
                        IdCaixa = reader.GetInt32("id_cai_fk"),
                        IdDespesa = reader.GetInt32("id_des_fk"),
                        IdCompra = reader.GetInt32("id_comp_fk")
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar os pagamentos.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Pagamento GetById(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL select_pagamento_id(@id);";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();
                Pagamento pagamento = null;
                if (reader.Read())
                {
                    pagamento = new Pagamento()
                    {
                        Id = reader.GetInt32("id"),
                        Status = reader.GetBoolean("status"),
                        Valor = reader.GetDouble("valor"),
                        Parcela = reader.GetInt32("parcela"),
                        Data = reader.GetDateTime("data"),
                        Forma = reader.GetString("forma"),
                        IdCaixa = reader.GetInt32("id_cai_fk"),
                        IdDespesa = reader.GetInt32("id_des_fk"),
                        IdCompra = reader.GetInt32("id_comp_fk")
                    };
                }
                return pagamento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar o pagamento.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Pagamento Update(int id, PagamentoDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL update_pagamento (@id, @status, @valor, @parcela, @data, @forma, @id_cai, @id_des, @id_comp)";

                query.Parameters.AddWithValue("@id", id);
                query.Parameters.AddWithValue("@status", item.Status);
                query.Parameters.AddWithValue("@valor", item.Valor);
                query.Parameters.AddWithValue("@parcela", item.Parcela);
                query.Parameters.AddWithValue("@data", item.Data);
                query.Parameters.AddWithValue("@forma", item.Forma);
                query.Parameters.AddWithValue("@id_cai", item.IdCaixa);
                query.Parameters.AddWithValue("@id_des", item.IdDespesa);
                query.Parameters.AddWithValue("@id_comp", item.IdCompra);

                var result = query.ExecuteNonQuery();
                if (result < 0)
                {
                    throw new Exception("Ocorreu um erro ao atualizar o pagamento.");
                }
                return GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o pagamento.");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}


