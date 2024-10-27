using System.ComponentModel.DataAnnotations;

namespace Product_Rent.DTOs
{
    public class CaixaDTO
    {
        [Required]
        public int Numero { get; set; }
        [Required]
        public decimal SaldoInicial { get; set; }
        [Required]
        public int FuncionarioId { get; set; } 
    }

}
