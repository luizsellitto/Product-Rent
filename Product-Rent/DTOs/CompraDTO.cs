using Product_Rent.Models;
using System.ComponentModel.DataAnnotations;

namespace Product_Rent.DTOs
{
    public class CompraDTO
    {
        public DateTime Data { get; set; }
        public double Valor_Total { get; set; }
        public string Forma_de_Pagamento { get; set; }
        public int Id_Fun { get; set; }
        public int Id_For { get; set; }
    }
}