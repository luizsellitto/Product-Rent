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
        [HttpDelete("{id}")]
        public IActionResult Close(int id, [FromBody] CaixaDTO item) //Arrumar: permite fechar caixa já fechado;
        {                                                            //Saldo final, recebimento e retiradas precisam 
            var caixa = new Caixa();                                 //ser escritas ou será automático do sistema?
            caixa.SaldoFinal = item.SaldoFinal;
            caixa.TotalRecebimentos = item.TotalRecebimentos;
            caixa.TotalRetiradas = item.TotalRetiradas;
            if(caixa.Status == "Fechado")   
            {
                return BadRequest("Este caixa já está fechado.");
            }
            try
            {
                var dao = new CaixaDAO().CloseCaixa(id, caixa);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

    }
}
