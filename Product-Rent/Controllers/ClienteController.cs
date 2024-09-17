using Atividade_ANP_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var clientes = ClienteOperacoes.Listar();
            return Ok(clientes);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var cliente = ClienteOperacoes.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost("Criar")]
        public IActionResult Criar([FromBody] ClienteDTO clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest("Cliente não pode ser vazio.");
            }

            if (clienteDto.CNPJ != "")
            { 
                if ((ValidarCNPJ.ValidaCnpj(clienteDto.CNPJ) == false))
                {
                    return BadRequest("CNPJ inválido.");
                }
            }
            
            
            if (clienteDto.CPF != "")
            {
                if (ValidarCPF.ValidaCPF(clienteDto.CPF) == false)
                {
                    return BadRequest("CPF inválido.");
                }
            }
            
            if (!(clienteDto.DataNascimento.Length == 10 &&
                char.IsDigit(clienteDto.DataNascimento[0]) && char.IsDigit(clienteDto.DataNascimento[1]) &&
                clienteDto.DataNascimento[2] == '/' &&
                char.IsDigit(clienteDto.DataNascimento[3]) && char.IsDigit(clienteDto.DataNascimento[4]) &&
                clienteDto.DataNascimento[5] == '/' &&
                char.IsDigit(clienteDto.DataNascimento[6]) && char.IsDigit(clienteDto.DataNascimento[7]) &&
                char.IsDigit(clienteDto.DataNascimento[8]) && char.IsDigit(clienteDto.DataNascimento[9])))
            {
                return BadRequest("Data Escrita Errado.");
            }

            var cliente = ClienteOperacoes.Criar(clienteDto);
            return Ok(cliente);
        }

        [HttpPut("AtualizarById")]
        public IActionResult Atualizar(int id, [FromBody] ClienteDTO clienteAtualizado)
        {
            if (clienteAtualizado == null)
            {
                return BadRequest("Cliente não pode ser vazio.");
            }
            if (!(clienteAtualizado.DataNascimento.Length == 10 &&
                char.IsDigit(clienteAtualizado.DataNascimento[0]) && char.IsDigit(clienteAtualizado.DataNascimento[1]) &&
                clienteAtualizado.DataNascimento[2] == '/' &&
                char.IsDigit(clienteAtualizado.DataNascimento[3]) && char.IsDigit(clienteAtualizado.DataNascimento[4]) &&
                clienteAtualizado.DataNascimento[5] == '/' &&
                char.IsDigit(clienteAtualizado.DataNascimento[6]) && char.IsDigit(clienteAtualizado.DataNascimento[7]) &&
                char.IsDigit(clienteAtualizado.DataNascimento[8]) && char.IsDigit(clienteAtualizado.DataNascimento[9])))
            {
                return BadRequest("Data Escrita Errado.");
            }

            if (clienteAtualizado.CNPJ != "")
            {
                if ((ValidarCNPJ.ValidaCnpj(clienteAtualizado.CNPJ) == false))
                {
                    return BadRequest("CNPJ inválido.");
                }
            }

            if (clienteAtualizado.CPF != "")
            {
                if (ValidarCPF.ValidaCPF(clienteAtualizado.CPF) == false)
                {
                    return BadRequest("CPF inválido.");
                }
            }

            var cliente = ClienteOperacoes.Atualizar(id, clienteAtualizado);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpDelete("DeletarById")]
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