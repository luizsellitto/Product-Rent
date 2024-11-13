using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AluguelController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] AluguelDTO item)
        {
            var aluguel = new AluguelDAO().Insert(item);

            try
            {
                var cliente = new ClienteDAO().GetById(item.IdCliente);
                var funcionario = new FuncionarioDAO().GetById(item.IdFuncionario);
                if (cliente == null || funcionario == null)
                {
                    return BadRequest("Cliente ou Funcionário não encontrados.");
                }
                if (item == null)
                {
                    return BadRequest("Aluguel não pode ser vazio.");
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok(aluguel);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var item = new AluguelDAO().GetAll();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var item = new AluguelDAO().GetById(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AluguelDTO item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Aluguel não pode ser vazio.");
                }
                var aluguel = new AluguelDAO().Update(id, item);
                if (aluguel == null)
                {
                    return NotFound();
                }

                return Ok(aluguel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Aluguel aluguel = new AluguelDAO().Inative(id);
                if (aluguel == null)
                {
                    return NotFound("Aluguel não encontrado.");
                }
                return NoContent();
            }
            catch(Exception ex) 
            { 
                return BadRequest(ex.Message) ;            
            } 
        }
    }
}