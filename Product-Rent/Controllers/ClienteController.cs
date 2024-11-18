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
        [HttpPost]
        public IActionResult Create([FromBody] ClienteDTO item)
        {
            try
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

                var cliente = new ClienteDAO().Insert(item);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var clientes = new ClienteDAO().GetAll();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = new ClienteDAO().GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClienteDTO item)
        {
            try
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
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            new ClienteDAO().Inative(id);

            return NoContent();
        }
    }
}