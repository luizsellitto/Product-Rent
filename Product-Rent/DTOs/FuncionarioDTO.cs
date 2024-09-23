using Product_Rent.Models;
using System.ComponentModel.DataAnnotations;

namespace Product_Rent.DTOs
{
    public class FuncionarioDTO
    {

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Rg { get; set; }

        [Required]
        public string Funcao { get; set; }

        [Required]
        public string Ctps { get; set; }

        [Required]
        public string Setor { get; set; }
        
        [Required]
        public string Sala { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public List<Endereco> Endereco { get; set; }
    }
}