using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Marca { get; set; }

        public string Tamanho { get; set; }

        public string Cor{ get; set; }

        public double Valor_aluguel { get; set; }

        public string Descricao { get; set; }

        public Fornecedor IdFornecedor { get; set; }
    }

}
