using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;
namespace Product_Rent.Models
{
    public class CompraDAO
    {
        private static ConnectionMysql conn;

        public CompraDAO()
        {
            conn = new ConnectionMysql();
        }

        public int Insert(CompraDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_compra (@data, @valor_total, @forma_de_pagamento, @id_fun_fk, @id_for_fk);";

                query.Parameters.AddWithValue("@data", item.Data.ToString("yyyy-MM-dd HH:mm:ss"));
                query.Parameters.AddWithValue("@valor_total", item.Valor_Total);
                query.Parameters.AddWithValue("@forma_de_pagamento", item.Forma_de_Pagamento);
                query.Parameters.AddWithValue("@id_fun_fk", item.Id_Fun);
                query.Parameters.AddWithValue("@id_for_fk", item.Id_For);

                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar inserir a compra.");
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Compra> GetAll()
        {
            try
            {
                List<Compra> list = new List<Compra>();
                var query = conn.Query();
                query.CommandText = "CALL select_compra();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Compra()
                    {
                        Id = reader.GetInt32("id"),
                        Data = reader.GetDateTime("data"),
                        Valor_Total = reader.GetDouble("valor_total"),
                        Forma_de_Pagamento = reader.GetString("forma_de_pagamento"),
                        Id_Fun = reader.GetInt32("id_fun_fk"),
                        Id_For = reader.GetInt32("id_for_fk"),
                        Status = true
                    });

                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar as compras.");
            }
            finally
            {
                conn.Close();
            }
        }

        public Compra GetById(int id)
        {
            try
            {
                Compra compra = null;
                var query = conn.Query();
                query.CommandText = "CALL select_compra_id(@id);";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {

                    compra = new Compra()
                    {
                        Id = reader.GetInt32("id"),
                        Data = reader.GetDateTime("data"),
                        Valor_Total = reader.GetDouble("valor_total"),
                        Forma_de_Pagamento = reader.GetString("forma_de_pagamento"),
                        Id_Fun = reader.GetInt32("id_fun_fk"),
                        Id_For = reader.GetInt32("id_for_fk"),
                        Status = true
                    };
                }
                return compra;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar a compra");
            }
            finally
            {
                conn.Close();
            }
        }
        public Compra Update(int id, CompraDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL update_compra(@id, @valor_total, @forma_de_pagamento);";
                query.Parameters.AddWithValue("id", id);
                query.Parameters.AddWithValue("@valor_total", item.Valor_Total);
                query.Parameters.AddWithValue("@forma_de_pagamento", item.Forma_de_Pagamento);

                var result = query.ExecuteNonQuery();

                if (result < 0)
                {
                    throw new Exception("Ocorreu um erro ao atualizar a compra.");
                }
                return GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar a compra.");
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
                query.CommandText = "CALL inative_compra(@id);";
                query.Parameters.AddWithValue("@id", id);
                query.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar desativar a compra.");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}