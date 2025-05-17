using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;

namespace MottuBracelet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoPatiosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoricoPatiosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoPatio>>> GetHistoricos()
        {
            return await _context.HistoricoPatio
                .Include(h => h.Moto)
                .Include(h => h.Patio)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoPatio>> GetHistorico(int id)
        {
            var historico = await _context.HistoricoPatio
                .Include(h => h.Moto)
                .Include(h => h.Patio)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historico == null)
                return NotFound();

            return historico;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorico(int id, HistoricoPatio historico)
        {
            if (id != historico.Id)
                return BadRequest();

            _context.Entry(historico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoricoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HistoricoPatio>> PostHistorico(HistoricoPatio historico)
        {
            _context.HistoricoPatio.Add(historico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorico), new { id = historico.Id }, historico);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorico(int id)
        {
            var historico = await _context.HistoricoPatio.FindAsync(id);
            if (historico == null)
                return NotFound();

            _context.HistoricoPatio.Remove(historico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoricoExists(int id)
        {
            return _context.HistoricoPatio.Any(e => e.Id == id);
        }
    }
}
