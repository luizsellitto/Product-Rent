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
        public string Cor { get; set; }

        [Required]
        public double preco_custo { get; set; }


        [Required]
        public double Valor_aluguel { get; set; }


        [Required]
        public string Descricao { get; set; }

    }
}
