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

        public List<Endereco> Endereco { get; set; }

        public string ContatoUm { get; set; }

        public string ContatoDois { get; set; }

        public string ContatoTres { get; set; }

        public Endereco EmailUm { get; set; }

        public Endereco EmailDois { get; set; }
    }
}
