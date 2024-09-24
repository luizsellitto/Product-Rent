using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var produtos = ProdutoOperacoes.Get();
            return Ok(produtos);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            var produto = ProdutoOperacoes.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var produto = ProdutoOperacoes.Create(produtoDto);
            return Ok(produto);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int id, [FromBody] ProdutoDTO produtoAtualizado)
        {
            if (produtoAtualizado == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var produto = ProdutoOperacoes.Update(id, produtoAtualizado);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            var verificar = ProdutoOperacoes.Delete(id);
            if (!verificar)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
