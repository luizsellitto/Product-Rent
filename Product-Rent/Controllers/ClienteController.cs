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
            var clientes = new ClienteDAO().GetAll();
            return Ok(clientes);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            var cliente = new ClienteDAO().GetById(id);
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

            var cliente = new ClienteDAO().Insert(clienteDto);
            return Ok(cliente);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int id, [FromBody] ClienteDTO item)
        {
            if (item == null)
            {
                return BadRequest("Cliente não pode ser vazio.");
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

            var cliente = new ClienteDAO().Update(id, item);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            new ClienteDAO().Inative(id);

            return NoContent();
        }
    }
}