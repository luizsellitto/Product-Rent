using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var produtos = ProdutoOperacoes.Listar();
            return Ok(produtos);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var produto = ProdutoOperacoes.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost("Criar")]
        public IActionResult Criar([FromBody] ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var produto = ProdutoOperacoes.Criar(produtoDto);
            return Ok(produto);
        }

        [HttpPut("AtualizarById")]
        public IActionResult Atualizar(int id, [FromBody] ProdutoDTO produtoAtualizado)
        {
            if (produtoAtualizado == null)
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            var produto = ProdutoOperacoes.Atualizar(id, produtoAtualizado);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpDelete("DeletarById")]
        public IActionResult Deletar(int id)
        {
            var verificar = ProdutoOperacoes.Deletar(id);
            if (!verificar)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
