using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string RG { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public List<Endereco> Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}



public static class ClienteOperacoes
{
    private static List<Cliente> clientes = new List<Cliente>();

    public static IEnumerable<Cliente> Listar()
    {
        return clientes;
    }

    public static Cliente GetById(int id)
    {
        return clientes.FirstOrDefault(c => c.Id == id);
    }

    public static Cliente Criar(ClienteDTO clienteDto)
    {
        var maiorId = clientes.Count > 0 ? clientes.Max(c => c.Id) : 0;
        var cliente = new Cliente
        {
            Id = maiorId + 1,
            Nome = clienteDto.Nome,
            DataNascimento = clienteDto.DataNascimento,
            Sexo = clienteDto.Sexo,
            RG = clienteDto.RG,
            CNPJ = clienteDto.CNPJ,
            CPF = clienteDto.CPF,
            Endereco = clienteDto.Endereco,
            Telefone = clienteDto.Telefone,
            Email = clienteDto.Email
        };

        clientes.Add(cliente);
        return cliente;
    }

    public static Cliente Atualizar(int id, ClienteDTO clienteDto)
    {
        var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);
        if (clienteExistente == null) return null;
        var cliente = new Cliente
        {
            Id = id,
            Nome = clienteDto.Nome,
            DataNascimento = clienteDto.DataNascimento,
            Sexo = clienteDto.Sexo,
            RG = clienteDto.RG,
            CNPJ = clienteDto.CNPJ,
            CPF = clienteDto.CPF,
            Endereco = clienteDto.Endereco,
            Telefone = clienteDto.Telefone,
            Email = clienteDto.Email
        };
        clientes.Remove(clienteExistente);
        clientes.Add(cliente);
        return cliente;
    }

    public static bool Deletar(int id)
    {
        var cliente = clientes.FirstOrDefault(c => c.Id == id);
        if (cliente == null) return false;

        clientes.Remove(cliente);
        return true;
    }
}