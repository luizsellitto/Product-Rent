using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] DespesaDTO despesaDto)
        {
            try
            {
                if (despesaDto == null)
                {
                    return BadRequest("Despesa não pode ser vazio.");
                }

                var despesa = new DespesaDAO().Insert(despesaDto);
                return Ok(despesa);
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
                var despesa = new DespesaDAO().GetAll();
                return Ok(despesa);
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
                var despesa = new DespesaDAO().GetById(id);
                if (despesa == null)
                {
                    return NotFound();
                }
                return Ok(despesa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DespesaDTO item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Despesa não pode ser vazio.");
                }

                var despesa = new DespesaDAO().Update(id, item);
                if (despesa == null)
                {
                    return NotFound();
                }

                return Ok(despesa);
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
                new DespesaDAO().Inative(id);

                return NoContent();
            }
            catch { return Problem(); }
        }
    }
}