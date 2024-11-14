using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Rent.DTOs;
using Product_Rent.Models;
using static Product_Rent.Models.Fornecedor;

namespace Product_Rent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] FornecedorDTO item)
        {
            
            var fornecedor = new Fornecedor();
            fornecedor.CNPJ = item.CNPJ;
            fornecedor.RazaoSocial = item.RazaoSocial;
            fornecedor.NomeFantasia = item.NomeFantasia;
            fornecedor.InscricaoEstadual = item.InscricaoEstadual;
            fornecedor.InscricaoMunicipal = item.InscricaoMunicipal;
            fornecedor.Responsavel = item.Responsavel;
            fornecedor.ContatoUm = item.ContatoUm;
            fornecedor.ContatoDois = item.ContatoDois;
            fornecedor.ContatoTres = item.ContatoTres;
            fornecedor.EmailUm = item.EmailUm;
            fornecedor.EmailDois = item.EmailDois;
            fornecedor.Endereco = item.Endereco;
            fornecedor.Status = true;

            try
            {
                var dao = new FornecedorDAO();
                fornecedor.Id = dao.Insert(fornecedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("", fornecedor);
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Fornecedor> fornecedores = new FornecedorDAO().GetAll();
                return Ok(fornecedores);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Fornecedor fornecedor = new FornecedorDAO().GetById(id);

                if (fornecedor == null)
                {
                    return NotFound();
                }
                return Ok(fornecedor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FornecedorDTO item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Produto não pode ser vazio.");
                }

                Fornecedor update = new FornecedorDAO().Update(id, item);
                if (update == null)
                {
                    return NotFound("Fornecedor não encontrado ou atualização falhou");
                }
                return Ok(update);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Fornecedor fornecedor = new FornecedorDAO().GetById(id);
                if (fornecedor == null || !fornecedor.Status)
                {
                    return BadRequest("O funcionário está inativo e não pode ser acessado.");

                }
                fornecedor = new FornecedorDAO().Inative(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}