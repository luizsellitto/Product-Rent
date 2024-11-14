using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecebimentoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] RecebimentoDTO item)
        {
            try
            {
                var recebimento = new RecebimentoDAO().Insert(item);

            
                var caixa = new CaixaDAO().GetById(item.Id_cai);
                var aluguel = new AluguelDAO().GetById(item.Id_alu);
                if (caixa == null || aluguel == null)
                {
                    return BadRequest("Caixa ou Aluguel não encontrados.");
                }
                if (item == null)
                {
                    return BadRequest("Recebimento não pode ser vazio.");
                }
                return Ok(recebimento);
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
                var item = new RecebimentoDAO().GetAll();
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
                var item = new RecebimentoDAO().GetById(id);
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
        public IActionResult Update(int id, [FromBody] RecebimentoDTO item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Recebimento não pode ser vazio.");
                }
                var recebimento = new RecebimentoDAO().Update(id, item);
                if (recebimento == null)
                {
                    return NotFound();
                }

                return Ok(recebimento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}