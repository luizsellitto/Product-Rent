using MySql.Data.MySqlClient;
using Product_Rent.DataBase;
using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class DespesaDAO
    {

            private static ConnectionMysql conn;

            public DespesaDAO()
            {
                conn = new ConnectionMysql();
            }

            public int Insert(DespesaDTO item)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = "CALL insert_despesa (@nome, @data, @vencimento, @parcelamento, @descricao);";

                    query.Parameters.AddWithValue("@nome", item.Nome);
                    query.Parameters.AddWithValue("@data", item.Data.ToString("yyyy-MM-dd HH:mm:ss"));
                    query.Parameters.AddWithValue("@vencimento", item.Vencimento.ToString("yyyy-MM-dd HH:mm:ss"));
                    query.Parameters.AddWithValue("@parcelamento", item.Parcelamento);
                    query.Parameters.AddWithValue("@descricao", item.Descricao);

                    var id = Convert.ToInt32(query.ExecuteScalar());
                    return id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro geral: {ex.Message}");
                    throw new Exception("Ocorreu um erro inesperado ao tentar inserir a despesa.");
                }
                finally
                {
                    conn.Close();
                }
            }

            public List<Despesa> GetAll()
            {
                try
                {
                    List<Despesa> list = new List<Despesa>();
                    var query = conn.Query();
                    query.CommandText = "CALL select_despesa();";
                    MySqlDataReader reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Despesa()
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome"),
                            Data = reader.GetDateTime("data"),
                            Vencimento = reader.GetDateTime("vencimento"),
                            Parcelamento = reader.GetInt32("parcelamento"),
                            Descricao = reader.GetString("descricao"),
                            Status = reader.GetBoolean("status")
                        });

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro geral: {ex.Message}");
                    throw new Exception("Ocorreu um erro inesperado ao tentar buscar as despesas.");
                }
                finally
                {
                    conn.Close();
                }
            }

            public Despesa GetById(int id)
            {
                try
                {
                    Despesa despesa = null;
                    var query = conn.Query();
                    query.CommandText = "CALL select_despesa_id(@id);";
                    query.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = query.ExecuteReader();

                    if (reader.Read())
                    {

                        despesa = new Despesa()
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome"),
                            Data = reader.GetDateTime("data"),
                            Vencimento = reader.GetDateTime("vencimento"),
                            Parcelamento = reader.GetInt32("parcelamento"),
                            Descricao = reader.GetString("descricao"),
                            Status = reader.GetBoolean("status")
                        };
                    }
                    return despesa;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro geral: {ex.Message}");
                    throw new Exception("Ocorreu um erro inesperado ao tentar buscar a despesa.");
                }
                finally
                {
                    conn.Close();
                }
            }
            public Despesa Update(int id, DespesaDTO item)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = "CALL update_despesa(@id, @nome, @data, @vencimento, @parcelamento, @descricao); ";

                    query.Parameters.AddWithValue("@id", id);
                    query.Parameters.AddWithValue("@nome", item.Nome);
                    query.Parameters.AddWithValue("@data", item.Data.ToString("yyyy-MM-dd HH:mm:ss"));
                    query.Parameters.AddWithValue("@vencimento", item.Vencimento.ToString("yyyy-MM-dd HH:mm:ss"));
                    query.Parameters.AddWithValue("@parcelamento", item.Parcelamento);
                    query.Parameters.AddWithValue("@descricao", item.Descricao);

                var result = query.ExecuteNonQuery();

                    if (result < 0)
                    {
                        throw new Exception("Ocorreu um erro ao atualizar a despesa.");
                    }
                    return GetById(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro geral: {ex.Message}");
                    throw new Exception("Ocorreu um erro inesperado ao tentar atualizar a despesa.");
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
                    query.CommandText = "CALL inative_despesa(@id);";
                    query.Parameters.AddWithValue("@id", id);
                    query.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro geral: {ex.Message}");
                    throw new Exception("Ocorreu um erro inesperado ao tentar desativar a despesa.");
                }
                finally
                {
                    conn.Close();
                }
            }

        
    }

}
