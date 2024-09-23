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
        public IActionResult Listar()
        {
            var Funcionarios = FuncionarioOperacoes.Listar();
            return Ok(Funcionarios);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            var funcionario = FuncionarioOperacoes.GetById(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] FuncionarioDTO item)
        {
            if (item == null)
            {
                return BadRequest("Cliente não pode ser vazio.");
            }

            if (item.Cpf != "")
            {
                if (ValidarCPF.ValidaCPF(item.Cpf) == false)
                {
                    return BadRequest("CPF inválido.");
                }
            }
            var funcionario = FuncionarioOperacoes.Criar(item);
            return Ok(funcionario);
        }

        [HttpPut("{Id}")]
        public IActionResult Atualizar(int id, [FromBody] FuncionarioDTO item)
        {
            if (item == null)
            {
                return NotFound();
            }
            
            if (item.Cpf != "")
            {
                if (ValidarCPF.ValidaCPF(item.Cpf) == false)
                {
                    return BadRequest("CPF inválido.");
                }
            }

            var funcionario = FuncionarioOperacoes.Atualizar(id, item);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpDelete("{Id}")]
        public IActionResult Deletar(int id)
        {
            var verificar = ClienteOperacoes.Deletar(id);
            if (!verificar)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
