using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] ProdutoDTO item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("produto não pode ser vazio.");
                }

                var produto = new ProdutoDAO().Insert(item);
                return Ok(produto);
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
                var produto = new ProdutoDAO().GetAll();
                return Ok(produto);
            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex);
                return BadRequest(ex.Message);

            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produto = new ProdutoDAO().GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProdutoDTO item)
        {
            if (item == null)
            {
                return BadRequest("produto não pode ser vazio.");
            }

           

            var produto = new ProdutoDAO().Update(id, item);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                new ProdutoDAO().Inative(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
