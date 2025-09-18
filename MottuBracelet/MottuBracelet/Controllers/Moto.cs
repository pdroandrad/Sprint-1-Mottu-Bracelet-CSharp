using Microsoft.AspNetCore.Mvc;
using MottuBracelet.Services;
using MottuBracelet.Model;
using MottuBracelet.DTO;

namespace MottuBracelet.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class MotoController : Controller 
    {
        private readonly ServicoMotos _servico; 

        
        public MotoController(ServicoMotos servico)
        {
            _servico = servico; 
        }

        // Método para obter todas as motos cadastradas, aceitando a paginação.
        [HttpGet] 
        public async Task<ActionResult<List<MotoHateoasDto>>> ObterTodos(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10)   
        {
            var motos = await _servico.ObterPaginadoAsync(pageNumber, pageSize); 
            var total = await _servico.ContarAsync(); 

            var urlBase = $"{Request.Scheme}://{Request.Host}/api"; 
            var motosDto = motos.Select(m => _servico.MontarMotoComLinks(m, urlBase)).ToList(); 

            Response.Headers.Add("X-Total-Count", total.ToString()); 

            return Ok(motosDto); 
        }

        // Método para obter uma moto pelo ID.
        [HttpGet("{id:int}", Name = "ObterMoto")] 
        public async Task<ActionResult<MotoHateoasDto>> ObterPorId(int id)
        {
            var moto = await _servico.ObterPorIdAsync(id); 
            if (moto == null) return NotFound(); 

            var urlBase = $"{Request.Scheme}://{Request.Host}/api"; 
            var motoHateoas = _servico.MontarMotoComLinks(moto, urlBase); 

            return Ok(motoHateoas); 
        }

        // Método para criar uma nova moto.
        [HttpPost] 
        public async Task<ActionResult<MotoHateoasDto>> Criar(Moto moto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState); 

            await _servico.CriarAsync(moto); 

            var urlBase = $"{Request.Scheme}://{Request.Host}/api"; 
            var motoHateoas = _servico.MontarMotoComLinks(moto, urlBase); 

            
            return CreatedAtRoute("ObterMoto", new { id = moto.Id }, motoHateoas);
        }

        // Método para atualizar os dados de uma moto existente.
        [HttpPut("{id:int}")] 
        public async Task<IActionResult> Atualizar(int id, Moto motoAtualizada)
        {
            var motoExistente = await _servico.ObterPorIdAsync(id); 
            if (motoExistente is null) return NotFound(); 

            motoAtualizada.Id = motoExistente.Id; 
            var atualizado = await _servico.AtualizarAsync(id, motoAtualizada); 

            return atualizado ? NoContent() : NotFound(); 
        }

        // Método para remover uma moto pelo ID.
        [HttpDelete("{id:int}")] 
        public async Task<IActionResult> Remover(int id)
        {
            var motoExistente = await _servico.ObterPorIdAsync(id); 
            if (motoExistente is null) return NotFound(); 

            var removido = await _servico.RemoverAsync(id); 
            return removido ? NoContent() : NotFound(); 
        }
    }
}
