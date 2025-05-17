using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;

namespace MottuBracelet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DispositivosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispositivo>>> GetDispositivos()
        {
            return await _context.Dispositivo
                .Include(d => d.Moto)
                .Include(d => d.Patio)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dispositivo>> GetDispositivo(int id)
        {
            var dispositivo = await _context.Dispositivo
                .Include(d => d.Moto)
                .Include(d => d.Patio)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dispositivo == null)
                return NotFound();

            return dispositivo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispositivo(int id, Dispositivo dispositivo)
        {
            if (id != dispositivo.Id)
                return BadRequest();

            _context.Entry(dispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Dispositivo>> PostDispositivo([FromBody] Dispositivo dispositivo)
        {
            // Limpa referências para evitar problemas
            dispositivo.Moto = null;
            dispositivo.Patio = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dispositivo.Add(dispositivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDispositivo), new { id = dispositivo.Id }, dispositivo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispositivo(int id)
        {
            var dispositivo = await _context.Dispositivo.FindAsync(id);
            if (dispositivo == null)
                return NotFound();

            _context.Dispositivo.Remove(dispositivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispositivoExists(int id)
        {
            return _context.Dispositivo.Any(e => e.Id == id);
        }
    }
}