using Product_Rent.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Product_Rent.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string InscricaoEstadual { get; set; }

        public string InscricaoMunicipal { get; set; }

        public string Responsavel { get; set; }

        public Endereco Endereco { get; set; }

        public string ContatoUm { get; set; }

        public string ContatoDois { get; set; }

        public string ContatoTres { get; set; }

        public string EmailUm { get; set; }

        public string EmailDois { get; set; }



        public static class FornecedorOperacoes
        {
            private static List<Fornecedor> fornecedores = new List<Fornecedor>();

            public static IEnumerable<Fornecedor> Listar()
            {
                return fornecedores;
            }

            public static Fornecedor GetById(int id)
            {
                return fornecedores.FirstOrDefault(c => c.Id == id);
            }

            public static Fornecedor Criar(FornecedorDTO item)
            {
                var maiorId = fornecedores.Count > 0 ? fornecedores.Max(c => c.Id) : 0;
                var fornecedor = new Fornecedor
                {
                    Id = maiorId + 1,
                    CNPJ = item.CNPJ,
                    RazaoSocial = item.RazaoSocial,
                    NomeFantasia = item.NomeFantasia,
                    InscricaoEstadual = item.InscricaoEstadual,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    Responsavel = item.Responsavel,
                    Endereco = item.Endereco,
                    ContatoUm = item.ContatoUm,
                    ContatoDois = item.ContatoDois,
                    ContatoTres = item.ContatoTres,
                    EmailUm = item.EmailUm,
                    EmailDois = item.EmailDois

                };

                fornecedores.Add(fornecedor);
                return fornecedor;
            }

            public static Fornecedor Atualizar(int id, FornecedorDTO item)
            {
                var fornecedorExistente = fornecedores.FirstOrDefault(c => c.Id == id);
                if (fornecedorExistente == null) return null;
                var fornecedor = new Fornecedor
                {
                    Id = id,
                    CNPJ = item.CNPJ,
                    RazaoSocial = item.RazaoSocial,
                    NomeFantasia = item.NomeFantasia,
                    InscricaoEstadual = item.InscricaoEstadual,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    Responsavel = item.Responsavel,
                    Endereco = item.Endereco,
                    ContatoUm = item.ContatoUm,
                    ContatoDois = item.ContatoDois,
                    ContatoTres = item.ContatoTres,
                    EmailUm = item.EmailUm,
                    EmailDois = item.EmailDois,
                };
                fornecedores.Remove(fornecedorExistente);
                fornecedores.Add(fornecedor);
                return fornecedor;
            }

            public static bool Deletar(int id)
            {
                var fornecedor = fornecedores.FirstOrDefault(c => c.Id == id);
                if (fornecedor == null) return false;

                fornecedores.Remove(fornecedor);
                return true;
            }
        }

    }
}