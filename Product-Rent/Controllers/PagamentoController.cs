using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] PagamentoDTO item)
        {
            try
            {         
                var caixa = new CaixaDAO().GetById(item.IdCaixa);
                var despesa = new DespesaDAO().GetById(item.IdDespesa);
                var compra = new CompraDAO().GetById(item.IdCompra);
                if (caixa == null || despesa == null || compra == null)
                {
                    return BadRequest("Caixa, Despesa ou Compra não encontrados.");
                }
                if (item == null)
                {
                    return BadRequest("Pagamento não pode ser vazio.");
                }
                var pagamento = new PagamentoDAO().Insert(item);

                return Ok(pagamento);
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
                var item = new PagamentoDAO().GetAll();
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
                var item = new PagamentoDAO().GetById(id);
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
        public IActionResult Update(int id, [FromBody] PagamentoDTO item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Pagamento não pode ser vazio.");
                }
                var pagamento = new PagamentoDAO().Update(id, item);
                if (pagamento == null)
                {
                    return NotFound();
                }

                return Ok(pagamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}