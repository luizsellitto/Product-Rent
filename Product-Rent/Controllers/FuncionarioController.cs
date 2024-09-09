using Product_Rent.Models;
using Product_Rent.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Product_Rent.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        List<Funcionario> listFuncionario = new List<Funcionario>();

        public FuncionarioController()
        {
            var funcionario1 = new Funcionario()
            {
                Id = 1,
                Nome = "Thiciane Fernanda Frata Borges"
            };

            var funcionario2 = new Funcionario()
            {
                Id = 2,
                Nome = "Júlio César Aguiar Guedes Pereira"
            };

            listFuncionario.Add(funcionario1);
            listFuncionario.Add(funcionario2);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var Funcionarios = listFuncionario;
            return Ok(Funcionarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var funcionario = listFuncionario.FirstOrDefault(item => item.Id == id);
            if (funcionario == null)
            {
                return NotFound(new { Mensagem = "O ID fornecido não existe." });
            }

            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Post([FromBody] FuncionarioDTO item)
        {
            var contador = listFuncionario.Count;
            var funcionario = new Funcionario
            {
                Id = contador + 1,
                Nome = item.Nome,
                Cpf = item.Cpf,
                Funcao = item.Funcao,
                Ctps = item.Ctps,
                Rg = item.Rg,
                Setor = item.Setor,
                Sala = item.Sala,
                Telefone = item.Telefone,
                Endereco = item.Endereco
            };

            //if (!VerificacaoCpf.ValidaCPF(item.Cpf))
            //{
            //    return BadRequest(new { Mensagem = "O CPF fornecido é inválido." });
            //}

            listFuncionario.Add(funcionario);
            return StatusCode(StatusCodes.Status201Created, funcionario);
        }

        [HttpPut("{id}")]
        public IActionResult PutById(int id, [FromBody] FuncionarioDTO item)
        {
            var funcionario = listFuncionario.FirstOrDefault(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            funcionario.Nome = item.Nome;
            funcionario.Cpf = item.Cpf;
            funcionario.Funcao = item.Funcao;
            funcionario.Ctps = item.Ctps;
            funcionario.Rg = item.Rg;
            funcionario.Setor = item.Setor;
            funcionario.Sala = item.Sala;
            funcionario.Telefone = item.Telefone;
            funcionario.Endereco = item.Endereco;

            //if (!VerificacaoCpf.ValidaCPF(item.Cpf))
            //{
            //    return BadRequest(new { Mensagem = "O CPF fornecido é inválido." });
            //}

            return Ok(funcionario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var funcionario = listFuncionario.FirstOrDefault(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound(new { Mensagem = "O ID fornecido não existe." });
            }

            listFuncionario.Remove(funcionario);
            return Ok(funcionario);
        }
    }
}
