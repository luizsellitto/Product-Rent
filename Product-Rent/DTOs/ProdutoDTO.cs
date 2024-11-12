using Product_Rent.Models;
using System.ComponentModel.DataAnnotations;

namespace Product_Rent.DTOs
{
    public class ProdutoDTO
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Tamanho { get; set; }

        [Required] 
        public string Cor { get; set; }

        [Required]
        public double Valor_aluguel { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public int IdFornecedor { get; set; }

    }
}
