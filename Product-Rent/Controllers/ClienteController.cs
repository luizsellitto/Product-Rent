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

        [HttpGet("GetByCPF")]
        public IActionResult GetByCPF(string cpf)
        {
            if (!(cpf.Length == 14 &&
                char.IsDigit(cpf[0]) && char.IsDigit(cpf[1]) && char.IsDigit(cpf[2]) &&
                cpf[3] == '.' &&
                char.IsDigit(cpf[4]) && char.IsDigit(cpf[5]) && char.IsDigit(cpf[6]) &&
                cpf[7] == '.' &&
                char.IsDigit(cpf[8]) && char.IsDigit(cpf[9]) && char.IsDigit(cpf[10]) &&
                cpf[11] == '-' &&
                char.IsDigit(cpf[12]) && char.IsDigit(cpf[13])))
            {
                return BadRequest("Modelo de CPF Errado, use a Mascara 000.000.000-00.");
            }
            if (ValidarCPF.ValidaCPF(cpf) == false)
            {
                return BadRequest("CPF inválido.");
            }
            var cliente = ClienteOperacoes.GetByCPF(cpf);
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
            if (!(clienteDto.CPF.Length == 14 &&
                char.IsDigit(clienteDto.CPF[0]) && char.IsDigit(clienteDto.CPF[1]) && char.IsDigit(clienteDto.CPF[2]) &&
                clienteDto.CPF[3] == '.' &&
                char.IsDigit(clienteDto.CPF[4]) && char.IsDigit(clienteDto.CPF[5]) && char.IsDigit(clienteDto.CPF[6]) &&
                clienteDto.CPF[7] == '.' &&
                char.IsDigit(clienteDto.CPF[8]) && char.IsDigit(clienteDto.CPF[9]) && char.IsDigit(clienteDto.CPF[10]) &&
                clienteDto.CPF[11] == '-' &&
                char.IsDigit(clienteDto.CPF[12]) && char.IsDigit(clienteDto.CPF[13])))
            {
                return BadRequest("Modelo de CPF Errado, use a Mascara 000.000.000-00.");
            }
            if (ValidarCPF.ValidaCPF(clienteDto.CPF) == false)
            {
                return BadRequest("CPF inválido.");
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
            if (!(clienteAtualizado.CPF.Length == 14 &&
                char.IsDigit(clienteAtualizado.CPF[0]) && char.IsDigit(clienteAtualizado.CPF[1]) && char.IsDigit(clienteAtualizado.CPF[2]) &&
                clienteAtualizado.CPF[3] == '.' &&
                char.IsDigit(clienteAtualizado.CPF[4]) && char.IsDigit(clienteAtualizado.CPF[5]) && char.IsDigit(clienteAtualizado.CPF[6]) &&
                clienteAtualizado.CPF[7] == '.' &&
                char.IsDigit(clienteAtualizado.CPF[8]) && char.IsDigit(clienteAtualizado.CPF[9]) && char.IsDigit(clienteAtualizado.CPF[10]) &&
                clienteAtualizado.CPF[11] == '-' &&
                char.IsDigit(clienteAtualizado.CPF[12]) && char.IsDigit(clienteAtualizado.CPF[13])))
            {
                return BadRequest("Modelo de CPF Errado, use a Mascara 000.000.000-00.");
            }
            if (ValidarCPF.ValidaCPF(clienteAtualizado.CPF) == false)
            {
                return BadRequest("CPF inválido.");
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