using Product_Rent.Models;
using Product_Rent.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Product_Rent.Controllers
{

    [ApiController]
    [Route("Api/[Controller]")]
    public class FuncionarioController : ControllerBase
    {
    [HttpGet]
    public IActionResult Get()
    {
        List<Funcionario> funcionarios = new FuncionarioDAO().GetAll();
        return Ok(funcionarios);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Funcionario funcionario = new FuncionarioDAO().GetById(id);
        return Ok(funcionario);
    }

    [HttpPost]
    public IActionResult Create([FromBody] FuncionarioDTO item)
    {
        //if (item.Cpf != "")
        //{
        //    if (ValidarCPF.ValidaCPF(item.Cpf) == false)
        //    {
        //        return BadRequest("CPF inválido.");
        //    }
        //}
        var funcionario = new Funcionario();

        funcionario.Nome = item.Nome;
        funcionario.Cpf = item.Cpf;
        funcionario.Rg = item.Rg;
        funcionario.Telefone = item.Rg;
        funcionario.Email = item.Email;
        funcionario.DataNascimento = item.DataNascimento;
        funcionario.Sexo = item.Sexo;
        funcionario.Ctps = item.Ctps;
        funcionario.Funcao = item.Funcao;
        funcionario.Endereco = item.Endereco;
        funcionario.Status = true;

        try
        {
            var dao = new FuncionarioDAO();
            funcionario.Id = dao.Insert(funcionario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Created("", funcionario);
        }
    [HttpPost]
    public IActionResult Create([FromBody] FuncionarioDTO item)
    {
        //if (item.Cpf != "")
        //{
        //    if (ValidarCPF.ValidaCPF(item.Cpf) == false)
        //    {
        //        return BadRequest("CPF inválido.");
        //    }
        //}
        var funcionario = new Funcionario();

        funcionario.Nome = item.Nome;
        funcionario.Cpf = item.Cpf;
        funcionario.Rg = item.Rg;
        funcionario.Telefone = item.Rg;
        funcionario.Email = item.Email;
        funcionario.DataNascimento = item.DataNascimento;
        funcionario.Sexo = item.Sexo;
        funcionario.Ctps = item.Ctps;
        funcionario.Funcao = item.Funcao;
        funcionario.Endereco = item.Endereco;
        funcionario.Status = true;

        try
        {
            var dao = new FuncionarioDAO();
            funcionario.Id = dao.Insert(funcionario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Created("", funcionario);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] FuncionarioDTO item)
    {
        if (item == null)
        {
            return NotFound();
        }

        //if (item.Cpf != "")
        //{
        //    if (ValidarCPF.ValidaCPF(item.Cpf) == false)
        //    {
        //        return BadRequest("CPF inválido.");
        //    }
        //}

        Funcionario update = new FuncionarioDAO().Update(id, item);
        if (update == null)
        {
            return NotFound("Funcionário não encontrado ou atualização falhou.");
        }
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Funcionario funcionario = new FuncionarioDAO().Delete(id);
        return NoContent();
    }
    }
}
