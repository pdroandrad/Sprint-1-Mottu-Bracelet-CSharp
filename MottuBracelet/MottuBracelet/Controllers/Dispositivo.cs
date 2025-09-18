using Microsoft.AspNetCore.Mvc;
using MottuBracelet.Services;
using MottuBracelet.Model;

namespace MottuBracelet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DispositivoController : Controller
    {
        private readonly ServicoDispositivos _servico;

        public DispositivoController(ServicoDispositivos servico)
        {
            _servico = servico;
        }

        // Método para obter todos os dispositivos cadastrados, aceitando a paginação.
        [HttpGet]
        public async Task<ActionResult<List<Dispositivo>>> ObterTodos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var dispositivos = await _servico.ObterPaginadoAsync(pageNumber, pageSize);
            var total = await _servico.ContarAsync();

            Response.Headers.Add("X-Total-Count", total.ToString());
            return dispositivos;
        }

        // Método para obter um dispositivo pelo ID.
        [HttpGet("{id:int}", Name = "ObterDispositivo")]
        public async Task<ActionResult<Dispositivo>> ObterPorId(int id)
        {
            var dispositivo = await _servico.ObterPorIdAsync(id);
            if (dispositivo == null) return NotFound();

            var urlBase = $"{Request.Scheme}://{Request.Host}/api";
            var dispositivoHateoas = _servico.MontarDispositivoComLinks(dispositivo, urlBase);

            return Ok(dispositivoHateoas);
        }

        // Método para criar um novo dispositivo.
        [HttpPost]
        public async Task<ActionResult<Dispositivo>> Criar(Dispositivo dispositivo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _servico.CriarAsync(dispositivo);
            return CreatedAtRoute("ObterDispositivo", new { id = dispositivo.Id }, dispositivo);
        }

        // Método para atualizar os dados de um dispositivo existente.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, Dispositivo dispositivoAtualizado)
        {
            var atualizado = await _servico.AtualizarAsync(id, dispositivoAtualizado);
            return atualizado ? NoContent() : NotFound();
        }

        // Método para remover um dispositivo pelo ID.
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remover(int id)
        {
            var removido = await _servico.RemoverAsync(id);
            return removido ? NoContent() : NotFound();
        }
    }
}
