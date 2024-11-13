namespace Product_Rent.DTOs
{
    public class DespesaDTO
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public DateTime Vencimento { get; set; }
        public int Parcelamento { get; set; }
        public string Descricao { get; set; }
    }
}
