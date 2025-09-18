using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.DTO;
using MottuBracelet.Model;

namespace MottuBracelet.Services
{
    public class ServicoPatios
    {
        private readonly AppDbContext _context;

        public ServicoPatios(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patio>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.Patio
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Patio.CountAsync();
        }

        public async Task<Patio?> ObterPorIdAsync(int id)
        {
            return await _context.Patio.FindAsync(id);
        }

        public async Task<Patio> CriarAsync(Patio patio)
        {
            _context.Patio.Add(patio);
            await _context.SaveChangesAsync();
            return patio;
        }

        public async Task<bool> AtualizarAsync(int id, Patio patioAtualizado)
        {
            var existe = await _context.Patio.AnyAsync(p => p.Id == id);
            if (!existe) return false;

            patioAtualizado.Id = id;
            _context.Entry(patioAtualizado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var patio = await _context.Patio.FindAsync(id);
            if (patio == null) return false;

            _context.Patio.Remove(patio);
            await _context.SaveChangesAsync();
            return true;
        }

        public PatioHateoasDto MontarPatioComLinks(Patio patio, string urlBase)
        {
            return new PatioHateoasDto
            {
                Id = patio.Id,
                Nome = patio.Nome,
                CapacidadeMaxima = patio.CapacidadeMaxima,
                AdministradorResponsavel = patio.AdministradorResponsavel,
                Endereco = patio.Endereco,
                Links = new List<LinkDto>
                {
                    new LinkDto { Href = $"{urlBase}/patio/{patio.Id}", Rel = "self", Method = "GET" },
                    new LinkDto { Href = $"{urlBase}/patio/{patio.Id}", Rel = "update", Method = "PUT" },
                    new LinkDto { Href = $"{urlBase}/patio/{patio.Id}", Rel = "delete", Method = "DELETE" }
                }
            };
        }
    }
}
