using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;

namespace MottuBracelet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Moto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetMotos()
        {
            return await _context.Moto
                .Include(m => m.Patio)
                .Include(m => m.Dispositivo)
                .ToListAsync();
        }

        // GET: api/Moto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetMoto(int id)
        {
            var moto = await _context.Moto
                .Include(m => m.Patio)
                .Include(m => m.Dispositivo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null)
            {
                return NotFound();
            }

            return moto;
        }

        // PUT: api/Moto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(int id, Moto moto)
        {
            if (id != moto.Id)
                return BadRequest();

            // Verifica se a moto existe
            var motoExistente = await _context.Moto.FindAsync(id);
            if (motoExistente == null)
                return NotFound();

            // Verifica se o Dispositivo informado existe
            var dispositivo = await _context.Dispositivo.FindAsync(moto.DispositivoId);
            if (dispositivo == null)
                return BadRequest("Dispositivo não encontrado.");

            // Atualiza os campos da moto
            motoExistente.Imei = moto.Imei;
            motoExistente.Placa = moto.Placa;
            motoExistente.PatioId = moto.PatioId;
            motoExistente.DispositivoId = moto.DispositivoId;

            // Atualiza o dispositivo para apontar para esta moto
            dispositivo.MotoId = moto.Id;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Moto
        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto(Moto moto)
        {
            // Verifica se o dispositivo existe
            var dispositivo = await _context.Dispositivo.FindAsync(moto.DispositivoId);
            if (dispositivo == null)
            {
                return BadRequest("Dispositivo não encontrado.");
            }

            // Cria a moto
            _context.Moto.Add(moto);
            await _context.SaveChangesAsync(); // Salva a moto para gerar o ID

            // Atualiza o dispositivo com o ID da moto recém-criada
            dispositivo.MotoId = moto.Id;
            _context.Dispositivo.Update(dispositivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoto", new { id = moto.Id }, moto);
        }

        // DELETE: api/Moto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            var moto = await _context.Moto.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }

            _context.Moto.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotoExists(int id)
        {
            return _context.Moto.Any(e => e.Id == id);
        }
    }
}