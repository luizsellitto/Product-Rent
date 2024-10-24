﻿using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Ctps { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }
        public string Sala { get; set; }
        public Endereco Endereco { get; set; }
        public bool Status {  get; set; }
    }
}


public static class FuncionarioOperacoes
{
    private static List<Funcionario> funcionarios = new List<Funcionario>();

    public static IEnumerable<Funcionario> Get()
    {
        return funcionarios;
    }

    public static Funcionario GetById(int id)
    {
        return funcionarios.FirstOrDefault(c => c.Id == id);
    }

    public static Funcionario Create(FuncionarioDTO item)
    {
        var maiorId = funcionarios.Count > 0 ? funcionarios.Max(c => c.Id) : 0;
        var funcionario = new Funcionario
        {
            Id = maiorId + 1,
            Nome = item.Nome,
            Cpf = item.Cpf,
            Funcao = item.Funcao,
            Ctps = item.Ctps,
            Rg = item.Rg,
            Setor = item.Setor,
            Sala = item.Sala,
            Telefone = item.Telefone,
            Endereco = item.Endereco
        };

        funcionarios.Add(funcionario);
        return funcionario;
    }

    public static Funcionario Update(int id, FuncionarioDTO item)
    {
        var funcionarioExistente = funcionarios.FirstOrDefault(c => c.Id == id);
        if (funcionarioExistente == null) return null;
        var funcionario = new Funcionario
        {
            Id = id,
            Nome = item.Nome,
            Cpf = item.Cpf,
            Funcao = item.Funcao,
            Ctps = item.Ctps,
            Rg = item.Rg,
            Setor = item.Setor,
            Sala = item.Sala,
            Telefone = item.Telefone,
            Endereco = item.Endereco
        };
        funcionarios.Remove(funcionarioExistente);
        funcionarios.Add(funcionario);
        return funcionario;
    }

    public static bool Delete(int id)
    {
        var funcionario = funcionarios.FirstOrDefault(c => c.Id == id);
        if (funcionario == null) return false;

        funcionarios.Remove(funcionario);
        return true;
    }
}