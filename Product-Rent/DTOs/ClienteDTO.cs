using Product_Rent.Models;
using System.ComponentModel.DataAnnotations;

namespace Product_Rent.DTOs
{
    public class ClienteDTO
    {
            [Required]
            public string Nome { get; set; }
            [Required]
            public string DataNascimento { get; set; }
            [Required]
            public string Sexo { get; set; }
            [Required]
            public string RG { get; set; }
            [Required]
            public string CPF { get; set; }
            [Required]
            public List<Endereco> Endereco { get; set; }
            [Required]
            public string Telefone { get; set; }
            [Required]
            public string Email { get; set; }
        
    }
}
