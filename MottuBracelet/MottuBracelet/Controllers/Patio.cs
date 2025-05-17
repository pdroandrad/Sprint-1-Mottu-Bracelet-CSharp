using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;

namespace MottuBracelet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatiosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatiosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patio>>> GetPatios()
        {
            return await _context.Patio.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> GetPatio(int id)
        {
            var patio = await _context.Patio.FindAsync(id);
            if (patio == null)
                return NotFound();

            return patio;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatio(int id, Patio patio)
        {
            if (id != patio.Id)
                return BadRequest();

            _context.Entry(patio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatioExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Patio>> PostPatio(Patio patio)
        {
            _context.Patio.Add(patio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatio), new { id = patio.Id }, patio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatio(int id)
        {
            var patio = await _context.Patio.FindAsync(id);
            if (patio == null)
                return NotFound();

            _context.Patio.Remove(patio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatioExists(int id)
        {
            return _context.Patio.Any(p => p.Id == id);
        }
    }
}
