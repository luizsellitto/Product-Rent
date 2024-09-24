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
        public IActionResult Listar()
        {
            var fornecedor = FornecedorOperacoes.Listar();

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
        public IActionResult Criar([FromBody] FornecedorDTO item)
        {
            if (item == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var fornece = FornecedorOperacoes.Criar(item);

            return Ok(fornece);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] FornecedorDTO item)
        {
            if (item == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var fornecedor = FornecedorOperacoes.Atualizar(id, item);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var verificar = FornecedorOperacoes.Deletar(id);

            if (!verificar)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}