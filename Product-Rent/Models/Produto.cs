using Product_Rent.DTOs;

namespace Product_Rent.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Marca { get; set; }

        public string Cor{ get; set; }

        public double preco_custo { get; set; }

        public double Valor_aluguel { get; set; }

        public string Descricao { get; set; }
    }

    public static class ProdutoOperacoes
    {
        private static List<Produto> produtos = new List<Produto>();

        public static IEnumerable<Produto> Get()
        {
            return produtos;
        }

        public static Produto GetById(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public static Produto Create(ProdutoDTO produtoDto)
        {
            var maiorId = produtos.Count > 0 ? produtos.Max(p => p.Id) : 0;
            var produto = new Produto
            {
                Id = maiorId + 1,
                Nome = produtoDto.Nome,
                Marca = produtoDto.Marca,
                Cor = produtoDto.Cor,
                preco_custo = produtoDto.preco_custo,
                Valor_aluguel = produtoDto.Valor_aluguel,
                Descricao = produtoDto.Descricao,
            };

            produtos.Add(produto);
            return produto;
        }

        public static Produto Update(int id, ProdutoDTO produtoDto)
        {
            var produtoExistente = produtos.FirstOrDefault(p => p.Id == id);
            if (produtoExistente == null) return null;

            var produtoAtualizado = new Produto
            {
                Id = id,
                Nome = produtoDto.Nome,
                Marca = produtoDto.Marca,
                Cor = produtoDto.Cor,
                preco_custo = produtoDto.preco_custo,
                Valor_aluguel = produtoDto.Valor_aluguel,
                Descricao = produtoDto.Descricao,
            };

            produtos.Remove(produtoExistente);
            produtos.Add(produtoAtualizado);
            return produtoAtualizado;
        }

        public static bool Delete(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return false;

            produtos.Remove(produto);
            return true;
        }
    }
}
