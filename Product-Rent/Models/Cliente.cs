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
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}



public static class ClienteOperacoes
{
    private static List<Cliente> clientes = new List<Cliente>();

    public static IEnumerable<Cliente> Get()
    {
        return clientes;
    }

    public static Cliente GetById(int id)
    {
        return clientes.FirstOrDefault(c => c.Id == id);
    }

    public static Cliente Create(ClienteDTO item)
    {
        var maiorId = clientes.Count > 0 ? clientes.Max(c => c.Id) : 0;
        var cliente = new Cliente
        {
            Id = maiorId + 1,
            Nome = item.Nome,
            DataNascimento = item.DataNascimento,
            Sexo = item.Sexo,
            RG = item.RG,
            CNPJ = item.CNPJ,
            CPF = item.CPF,
            Endereco = item.Endereco,
            Telefone = item.Telefone,
            Email = item.Email
        };

        clientes.Add(cliente);
        return cliente;
    }

    public static Cliente Update(int id, ClienteDTO item)
    {
        var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);
        if (clienteExistente == null) return null;
        var cliente = new Cliente
        {
            Id = id,
            Nome = item.Nome,
            DataNascimento = item.DataNascimento,
            Sexo = item.Sexo,
            RG = item.RG,
            CNPJ = item.CNPJ,
            CPF = item.CPF,
            Endereco = item.Endereco,
            Telefone = item.Telefone,
            Email = item.Email
        };
        clientes.Remove(clienteExistente);
        clientes.Add(cliente);
        return cliente;
    }

    public static bool Delete(int id)
    {
        var cliente = clientes.FirstOrDefault(c => c.Id == id);
        if (cliente == null) return false;

        clientes.Remove(cliente);
        return true;
    }
}