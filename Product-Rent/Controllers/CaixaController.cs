using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;

namespace Product_Rent.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class CaixaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Open([FromBody] CaixaDTO item)
        {
            var caixa = new Caixa();
            caixa.Numero = item.Numero;
            //caixa.Data = item.Data;
            caixa.SaldoInicial = item.SaldoInicial;
            caixa.FuncionarioId = item.FuncionarioId;

            try
            {
                var dao = new CaixaDAO();
                caixa.Id = dao.OpenCaixa(caixa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("", caixa);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var caixa = new CaixaDAO().GetById(id);
                if (caixa == null)
                {
                    return NotFound("Caixa não encontrado.");
                }
                return Ok(caixa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Close(int id, [FromBody] CaixaDTO item) //Arrumar: permite fechar caixa já fechado;
        {                                                            //Saldo final, recebimento e retiradas precisam 
            var caixa = new Caixa();                                 //ser escritas ou será automático do sistema?
            caixa.SaldoFinal = item.SaldoFinal;
            caixa.TotalRecebimentos = item.TotalRecebimentos;
            caixa.TotalRetiradas = item.TotalRetiradas;
            try
            {
                var dao = new CaixaDAO().CloseCaixa(id, caixa);
                if (dao == null)
                {
                    return NotFound("Caixa não encontrado.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

    }
}
