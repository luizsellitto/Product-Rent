using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] CompraDTO compraDto)
        {
            try
            {
                if (compraDto == null)
                {
                    return BadRequest("Compra não pode ser vazio.");
                }

                var compra = new CompraDAO().Insert(compraDto);
                return Ok(compra);
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
                var compra = new CompraDAO().GetAll();
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var compra = new CompraDAO().GetById(id);
                if (compra == null)
                {
                    return NotFound();
                }
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CompraDTO item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Compra não pode ser vazio.");
                }

                var compra = new CompraDAO().Update(id, item);
                if (compra == null)
                {
                    return NotFound();
                }

                return Ok(compra);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                new CompraDAO().Inative(id);

                return NoContent();
            }
            catch { return Problem(); }
        }
    }
}