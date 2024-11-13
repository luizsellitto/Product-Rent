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
            if (item == null)
            {
                return BadRequest("Aluguel não pode ser vazio.");
            }
            var aluguel = new AluguelDAO().Insert(item);
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
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = new AluguelDAO().GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
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
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            new AluguelDAO().Inative(id);

            return NoContent();
        }
    }
}