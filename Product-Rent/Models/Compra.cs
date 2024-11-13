using System.Security.Cryptography.Xml;

namespace Product_Rent.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor_Total { get; set; }
        public string Forma_de_Pagamento { get; set; }
        public bool Status { get; set; }
        public int Id_Fun { get; set; }
        public int Id_For { get; set; }
    }
}