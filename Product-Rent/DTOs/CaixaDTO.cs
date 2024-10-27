namespace Product_Rent.DTOs
{
    public class CaixaDTO
    {
        public int Numero { get; set; }
        public DateTime Data { get; set; } = DateTime.Today;
        public decimal SaldoInicial { get; set; }
        public int FuncionarioId { get; set; } 
    }

}
