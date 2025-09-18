using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.DTO;
using MottuBracelet.Model;

namespace MottuBracelet.Services
{
    public class ServicoDispositivos
    {
        private readonly AppDbContext _context;

        public ServicoDispositivos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dispositivo>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.Dispositivo
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Dispositivo.CountAsync();
        }

        public async Task<Dispositivo?> ObterPorIdAsync(int id)
        {
            return await _context.Dispositivo.FindAsync(id);
        }

        public async Task<Dispositivo> CriarAsync(Dispositivo dispositivo)
        {
            _context.Dispositivo.Add(dispositivo);
            await _context.SaveChangesAsync();
            return dispositivo;
        }

        public async Task<bool> AtualizarAsync(int id, Dispositivo dispositivoAtualizado)
        {
            var existe = await _context.Dispositivo.AnyAsync(d => d.Id == id);
            if (!existe) return false;

            dispositivoAtualizado.Id = id;
            _context.Entry(dispositivoAtualizado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var dispositivo = await _context.Dispositivo.FindAsync(id);
            if (dispositivo == null) return false;

            _context.Dispositivo.Remove(dispositivo);
            await _context.SaveChangesAsync();
            return true;
        }

        public DispositivoHateoasDto MontarDispositivoComLinks(Dispositivo dispositivo, string urlBase)
        {
            return new DispositivoHateoasDto
            {
                Id = dispositivo.Id,
                StatusDispositivo = dispositivo.StatusDispositivo,
                MotoId = dispositivo.MotoId,
                PatioId = dispositivo.PatioId,
                Links = new List<LinkDto>
                {
                    new LinkDto { Href = $"{urlBase}/dispositivo/{dispositivo.Id}", Rel = "self", Method = "GET" },
                    new LinkDto { Href = $"{urlBase}/dispositivo/{dispositivo.Id}", Rel = "update", Method = "PUT" },
                    new LinkDto { Href = $"{urlBase}/dispositivo/{dispositivo.Id}", Rel = "delete", Method = "DELETE" }
                }
            };
        }
    }
}
