namespace Product_Rent.Models
{
    public class Aluguel
    {
        public int Id { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public double ValorTotal { get; set; }
        public int IdCliente { get; set; }
        public int IdFuncionario { get; set; }

    }
}
