using Microsoft.AspNetCore.Mvc;
using MottuBracelet.Services;
using MottuBracelet.Model;

namespace MottuBracelet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioController : Controller
    {
        private readonly ServicoPatios _servico;

        public PatioController(ServicoPatios servico)
        {
            _servico = servico;
        }

        // Método para obter todos os pátios cadastrados, aceitando a paginação.
        [HttpGet]
        public async Task<ActionResult<List<Patio>>> ObterTodos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var patios = await _servico.ObterPaginadoAsync(pageNumber, pageSize);
            var total = await _servico.ContarAsync();

            Response.Headers.Add("X-Total-Count", total.ToString());
            return patios;
        }

        // Método para obter um pátio pelo ID.
        [HttpGet("{id:int}", Name = "ObterPatio")]
        public async Task<ActionResult<Patio>> ObterPorId(int id)
        {
            var patio = await _servico.ObterPorIdAsync(id);
            if (patio == null) return NotFound();

            var urlBase = $"{Request.Scheme}://{Request.Host}/api";
            var patioHateoas = _servico.MontarPatioComLinks(patio, urlBase);

            return Ok(patioHateoas);
        }

        // Método para criar um novo pátio.
        [HttpPost]
        public async Task<ActionResult<Patio>> Criar(Patio patio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _servico.CriarAsync(patio);
            return CreatedAtRoute("ObterPatio", new { id = patio.Id }, patio);
        }

        // Método para atualizar os dados de um pátio existente.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, Patio patioAtualizado)
        {
            var atualizado = await _servico.AtualizarAsync(id, patioAtualizado);
            return atualizado ? NoContent() : NotFound();
        }

        // Método para remover um pátio pelo ID.
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remover(int id)
        {
            var removido = await _servico.RemoverAsync(id);
            return removido ? NoContent() : NotFound();
        }
    }
}
