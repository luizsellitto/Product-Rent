using Product_Rent.Models;
namespace Product_Rent.DTOs
{
    public class AluguelDTO
    {
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public double ValorTotal { get; set; }
        public int IdCliente { get; set; }
        public int IdFuncionario { get; set; }
    }
}
