namespace Product_Rent.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public bool Status { get; set; } 
        public double Valor { get; set; }
        public int Parcela { get; set; }
        public DateTime Data { get; set; }
        public string Forma { get; set; }
        public int IdCaixa { get; set; }
        public int IdDespesa { get; set; }
        public int IdCompra { get; set; }
    }
}
