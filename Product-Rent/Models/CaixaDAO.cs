using Product_Rent.DataBase;
using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class CaixaDAO
    {
        private static ConnectionMysql conn;

        public CaixaDAO() 
        { 
            conn = new ConnectionMysql();
        }
        public int OpenCaixa(Caixa item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL open_caixa(@numero, @data, @saldo_inicial, 'Aberto', @id_fun_fk);";

                query.Parameters.AddWithValue("@numero", item.Numero);
                query.Parameters.AddWithValue("@data", item.Data);
                query.Parameters.AddWithValue("@saldo_inicial", item.SaldoInicial);
                query.Parameters.AddWithValue("Aberto", item.Status);
                query.Parameters.AddWithValue("@id_fun_fk", item.FuncionarioId);

                var id = Convert.ToInt32(query.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Erro inesperado ao abrir o caixa");
            }
            finally
            {
                conn.Close();
            }
        }
        public Caixa CloseCaixa(int id, Caixa item) 
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "CALL close_caixa(@id, @saldo_final, @total_recebimentos, @total_retiradas, 'Fechado');";

                query.Parameters.AddWithValue("@id", id);
                query.Parameters.AddWithValue("@saldo_final", item.SaldoFinal);
                query.Parameters.AddWithValue("@total_recebimentos", item.TotalRecebimentos);
                query.Parameters.AddWithValue("@total_retiradas", item.TotalRetiradas);
                query.Parameters.AddWithValue("Fechado", item.Status);

                query.ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                throw new Exception("Erro ao fechar o caixa");
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
