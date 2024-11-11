using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string RG { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}