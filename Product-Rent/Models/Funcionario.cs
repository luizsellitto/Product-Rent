using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace Product_Rent.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Ctps { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }
        public string Sala { get; set; }
        public Endereco Endereco { get; set; }
    }
}
