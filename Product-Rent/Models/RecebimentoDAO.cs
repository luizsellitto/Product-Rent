using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class RecebimentoDAO
    {
        private static ConnectionMysql conn;

        public RecebimentoDAO()
        {
            conn = new ConnectionMysql();
        }
        public int Insert(RecebimentoDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL insert_recebimento (@status, @valor, @parcela, @data, @forma, @vencimento, @id_cai, @id_alu)";

                query.Parameters.AddWithValue("@status", item.Status);
                query.Parameters.AddWithValue("@valor", item.Valor);
                query.Parameters.AddWithValue("@parcela", item.Parcela);
                query.Parameters.AddWithValue("@data", item.Data);
                query.Parameters.AddWithValue("@forma", item.Forma);
                query.Parameters.AddWithValue("@vencimento", item.Vencimento);
                query.Parameters.AddWithValue("@id_cai", item.Id_cai);
                query.Parameters.AddWithValue("@id_alu", item.Id_alu);

                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar cadastrar o recebimento.");
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Recebimento> GetAll()
        {
            try
            {
                List<Recebimento> list = new List<Recebimento>();
                var query = conn.Query();
                query.CommandText = "CALL select_recebimento();";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Recebimento()
                    {
                        Id = reader.GetInt32("id"),
                        Status = reader.GetString("status"),
                        Valor = reader.GetDouble("valor"),
                        Parcela = reader.GetInt32("parcela"),
                        Data = reader.GetDateTime("data"),
                        Forma = reader.GetString("forma"),
                        Vencimento = reader.GetDateTime("vencimento"),
                        Id_cai = reader.GetInt32("id_cai_fk"),
                        Id_alu = reader.GetInt32("id_alu_fk")
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar os recebimentos.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Recebimento GetById(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL select_recebimento_id(@id);";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();
                Recebimento recebimento = null;
                if (reader.Read())
                {
                    recebimento = new Recebimento()
                    {
                        Id = reader.GetInt32("id"),
                        Status = reader.GetString("status"),
                        Valor = reader.GetDouble("valor"),
                        Parcela = reader.GetInt32("parcela"),
                        Data = reader.GetDateTime("data"),
                        Forma = reader.GetString("forma"),
                        Vencimento = reader.GetDateTime("vencimento"),
                        Id_cai = reader.GetInt32("id_cai_fk"),
                        Id_alu = reader.GetInt32("id_alu_fk"),
                    };
                }
                return recebimento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar buscar o recebimento.");
            }
            finally
            {
                conn.Close();
            }
        }
        public Recebimento Update(int id, RecebimentoDTO item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL update_recebimento (@id, @status, @valor, @parcela, @data, @forma, @vencimento, @id_cai, @id_alu)";

                query.Parameters.AddWithValue("@id", id);
                query.Parameters.AddWithValue("@status", item.Status);
                query.Parameters.AddWithValue("@valor", item.Valor);
                query.Parameters.AddWithValue("@parcela", item.Parcela);
                query.Parameters.AddWithValue("@data", item.Data);
                query.Parameters.AddWithValue("@forma", item.Forma);
                query.Parameters.AddWithValue("@vencimento", item.Vencimento);
                query.Parameters.AddWithValue("@id_cai", item.Id_cai);
                query.Parameters.AddWithValue("@id_alu", item.Id_alu);

                var result = query.ExecuteNonQuery();
                if (result < 0)
                {
                    throw new Exception("Ocorreu um erro ao atualizar o recebimento.");
                }
                return GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o recebimento.");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
