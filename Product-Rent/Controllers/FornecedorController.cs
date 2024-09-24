using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using static Product_Rent.Models.Fornecedor;

namespace Product_Rent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get()
        {
            var fornecedor = FornecedorOperacoes.Get();

            return Ok(fornecedor);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var fornecedor = FornecedorOperacoes.GetById(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        [HttpPost()]
        public IActionResult Create([FromBody] FornecedorDTO item)
        {
            if (item == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var fornece = FornecedorOperacoes.Create(item);

            return Ok(fornece);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FornecedorDTO item)
        {
            if (item == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var fornecedor = FornecedorOperacoes.Update(id, item);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var verificar = FornecedorOperacoes.Delete(id);

            if (!verificar)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}