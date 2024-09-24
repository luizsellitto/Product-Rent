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
        [HttpGet]
        public IActionResult Get()
        {
            var clientes = ClienteOperacoes.Get();
            return Ok(clientes);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            var cliente = ClienteOperacoes.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClienteDTO clienteDto)
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

            var cliente = ClienteOperacoes.Create(clienteDto);
            return Ok(cliente);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int id, [FromBody] ClienteDTO item)
        {
            if (item == null)
            {
                return BadRequest("Cliente não pode ser vazio.");
            }
            if (!(item.DataNascimento.Length == 10 &&
                char.IsDigit(item.DataNascimento[0]) && char.IsDigit(item.DataNascimento[1]) &&
                item.DataNascimento[2] == '/' &&
                char.IsDigit(item.DataNascimento[3]) && char.IsDigit(item.DataNascimento[4]) &&
                item.DataNascimento[5] == '/' &&
                char.IsDigit(item.DataNascimento[6]) && char.IsDigit(item.DataNascimento[7]) &&
                char.IsDigit(item.DataNascimento[8]) && char.IsDigit(item.DataNascimento[9])))
            {
                return BadRequest("Data Escrita Errado.");
            }

            if (item.CNPJ != "")
            {
                if ((ValidarCNPJ.ValidaCnpj(item.CNPJ) == false))
                {
                    return BadRequest("CNPJ inválido.");
                }
            }

            if (item.CPF != "")
            {
                if (ValidarCPF.ValidaCPF(item.CPF) == false)
                {
                    return BadRequest("CPF inválido.");
                }
            }

            var cliente = ClienteOperacoes.Update(id, item);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            var verificar = ClienteOperacoes.Delete(id);
            if (!verificar)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}