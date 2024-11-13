namespace Product_Rent.DTOs
{
    public class RecebimentoDTO
    {
        public string Status { get; set; }
        public double Valor { get; set; }
        public int Parcela { get; set; }
        public DateTime Data { get; set; }
        public string Forma { get; set; }
        public DateTime Vencimento { get; set; }
        public int Id_cai { get; set; }
        public int Id_alu { get; set; }
    }
}
