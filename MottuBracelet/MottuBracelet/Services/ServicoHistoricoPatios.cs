using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.DTO;
using MottuBracelet.Model;

namespace MottuBracelet.Services
{
    public class ServicoHistoricoPatios
    {
        private readonly AppDbContext _context;

        public ServicoHistoricoPatios(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HistoricoPatio>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.HistoricoPatio
                .Include(h => h.Moto)
                .Include(h => h.Patio)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.HistoricoPatio.CountAsync();
        }

        public async Task<HistoricoPatio?> ObterPorIdAsync(int id)
        {
            return await _context.HistoricoPatio
                .Include(h => h.Moto)
                .Include(h => h.Patio)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<HistoricoPatio> CriarAsync(HistoricoPatio historico)
        {
            _context.HistoricoPatio.Add(historico);
            await _context.SaveChangesAsync();
            return historico;
        }

        public async Task<bool> AtualizarAsync(int id, HistoricoPatio historicoAtualizado)
        {
            var existe = await _context.HistoricoPatio.AnyAsync(h => h.Id == id);
            if (!existe) return false;

            historicoAtualizado.Id = id;
            _context.Entry(historicoAtualizado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var historico = await _context.HistoricoPatio.FindAsync(id);
            if (historico == null) return false;

            _context.HistoricoPatio.Remove(historico);
            await _context.SaveChangesAsync();
            return true;
        }

        public HistoricoPatioHateoasDto MontarHistoricoComLinks(HistoricoPatio historico, string urlBase)
        {
            return new HistoricoPatioHateoasDto
            {
                Id = historico.Id,
                MotoId = historico.MotoId,
                PatioId = historico.PatioId,
                DataEntrada = historico.DataEntrada,
                DataSaida = historico.DataSaida,
                Links = new List<LinkDto>
                {
                    new LinkDto { Href = $"{urlBase}/historicopatio/{historico.Id}", Rel = "self", Method = "GET" },
                    new LinkDto { Href = $"{urlBase}/historicopatio/{historico.Id}", Rel = "update", Method = "PUT" },
                    new LinkDto { Href = $"{urlBase}/historicopatio/{historico.Id}", Rel = "delete", Method = "DELETE" }
                }
            };
        }
    }
}
