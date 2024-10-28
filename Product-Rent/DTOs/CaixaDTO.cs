using System.ComponentModel.DataAnnotations;

namespace Product_Rent.DTOs
{
    public class CaixaDTO
    {
        public int Numero { get; set; }
        public decimal SaldoInicial { get; set; }
        [Required]
        public decimal SaldoFinal { get; set;}
        [Required]
        public decimal TotalRecebimentos { get; set; }
        [Required]
        public decimal TotalRetiradas { get; set; }
        public int FuncionarioId { get; set; } 
    }

}
