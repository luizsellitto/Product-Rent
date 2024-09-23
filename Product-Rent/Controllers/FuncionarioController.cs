using Product_Rent.Models;
using Product_Rent.DTOs;
using Microsoft.AspNetCore.Mvc;
using Atividade_ANP_API.Models;

namespace Product_Rent.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var Funcionarios = FuncionarioOperacoes.Listar();
            return Ok(Funcionarios);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var funcionario = FuncionarioOperacoes.GetById(id);
            if (funcionario == null)
            {
                return NotFound(new { Mensagem = "O ID fornecido não existe." });
            }

            return Ok(funcionario);
        }

        [HttpPost("Criar")]
        public IActionResult Criar([FromBody] FuncionarioDTO funcionarioDTO)
        {
            if (funcionarioDTO == null)
            {
                return BadRequest("Cliente não pode ser vazio.");
            }

            if (funcionarioDTO.Cpf != "")
            {
                if (ValidarCPF.ValidaCPF(funcionarioDTO.Cpf) == false)
                {
                    return BadRequest("CPF inválido.");
                }
            }
            var funcionario = FuncionarioOperacoes.Criar(funcionarioDTO);
            return Ok(funcionario);
        }

        [HttpPut("AtualizarById")]
        public IActionResult Atualizar(int id, [FromBody] FuncionarioDTO funcionarioDTO)
        {
            if (funcionarioDTO == null)
            {
                return NotFound(new { Mensagem = "O ID fornecido não existe." });
            }
            
            if (funcionarioDTO.Cpf != "")
            {
                if (ValidarCPF.ValidaCPF(funcionarioDTO.Cpf) == false)
                {
                    return BadRequest("CPF inválido.");
                }
            }

            var funcionario = FuncionarioOperacoes.Atualizar(id, funcionarioDTO);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpDelete("DeletarById")]
        public IActionResult Deletar(int id)
        {
            var verificar = ClienteOperacoes.Deletar(id);
            if (!verificar)
            {
                return NotFound(new { Mensagem = "O ID fornecido não existe." });
            }

            return NoContent();
        }
    }
}
