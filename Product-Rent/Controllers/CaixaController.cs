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
        public IActionResult Create([FromBody] CaixaDTO item)
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
    }
}
