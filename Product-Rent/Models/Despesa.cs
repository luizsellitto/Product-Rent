namespace Product_Rent.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public DateTime Vencimento { get; set; }
        public int Parcelamento { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
    }
}
