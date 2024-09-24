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
            var Funcionarios = FuncionarioOperacoes.Get();
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
        public IActionResult Create([FromBody] FuncionarioDTO item)
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
            var funcionario = FuncionarioOperacoes.Create(item);
            return Ok(funcionario);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int id, [FromBody] FuncionarioDTO item)
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

            var funcionario = FuncionarioOperacoes.Update(id, item);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            var verificar = FuncionarioOperacoes.Delete(id);
            if (!verificar)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
