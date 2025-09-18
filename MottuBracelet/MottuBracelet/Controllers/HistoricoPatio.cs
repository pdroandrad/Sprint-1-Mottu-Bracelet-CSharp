using Microsoft.AspNetCore.Mvc;
using MottuBracelet.Services;
using MottuBracelet.Model;

namespace MottuBracelet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoPatioController : Controller
    {
        private readonly ServicoHistoricoPatios _servico;

        public HistoricoPatioController(ServicoHistoricoPatios servico)
        {
            _servico = servico;
        }

        // Método para obter todos os históricos cadastrados, aceitando a paginação.
        [HttpGet]
        public async Task<ActionResult<List<HistoricoPatio>>> ObterTodos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var historicos = await _servico.ObterPaginadoAsync(pageNumber, pageSize);
            var total = await _servico.ContarAsync();

            Response.Headers.Add("X-Total-Count", total.ToString());
            return historicos;
        }

        // Método para obter um histórico pelo ID.
        [HttpGet("{id:int}", Name = "ObterHistoricoPatio")]
        public async Task<ActionResult<HistoricoPatio>> ObterPorId(int id)
        {
            var historico = await _servico.ObterPorIdAsync(id);
            if (historico == null) return NotFound();

            var urlBase = $"{Request.Scheme}://{Request.Host}/api";
            var historicoHateoas = _servico.MontarHistoricoComLinks(historico, urlBase);

            return Ok(historicoHateoas);
        }

        // Método para criar um novo histórico.
        [HttpPost]
        public async Task<ActionResult<HistoricoPatio>> Criar(HistoricoPatio historico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _servico.CriarAsync(historico);
            return CreatedAtRoute("ObterHistoricoPatio", new { id = historico.Id }, historico);
        }

        // Método para atualizar os dados de um histórico existente.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, HistoricoPatio historicoAtualizado)
        {
            var atualizado = await _servico.AtualizarAsync(id, historicoAtualizado);
            return atualizado ? NoContent() : NotFound();
        }

        // Método para remover um histórico pelo ID.
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remover(int id)
        {
            var removido = await _servico.RemoverAsync(id);
            return removido ? NoContent() : NotFound();
        }
    }
}
