using MottuBracelet.Model;
using MottuBracelet.DTO;
using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;

namespace MottuBracelet.Services
{
    public class ServicoMotos
    {
        private readonly AppDbContext _context;

        public ServicoMotos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Moto>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.Moto
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Moto.CountAsync();
        }

        public async Task<Moto?> ObterPorIdAsync(int id)
        {
            return await _context.Moto.FindAsync(id);
        }

        public async Task<Moto> CriarAsync(Moto moto)
        {
            _context.Moto.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<bool> AtualizarAsync(int id, Moto motoAtualizada)
        {
            var existe = await _context.Moto.AnyAsync(m => m.Id == id);
            if (!existe) return false;

            motoAtualizada.Id = id;
            _context.Entry(motoAtualizada).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var moto = await _context.Moto.FindAsync(id);
            if (moto == null) return false;

            _context.Moto.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }

        public MotoHateoasDto MontarMotoComLinks(Moto moto, string urlBase)
        {
            return new MotoHateoasDto
            {
                Id = moto.Id,
                Imei = moto.Imei,
                Placa = moto.Placa,
                Links = new List<LinkDto>
                {
                    new LinkDto { Href = $"{urlBase}/moto/{moto.Id}", Rel = "self", Method = "GET" },
                    new LinkDto { Href = $"{urlBase}/moto/{moto.Id}", Rel = "update", Method = "PUT" },
                    new LinkDto { Href = $"{urlBase}/moto/{moto.Id}", Rel = "delete", Method = "DELETE" }
                }
            };
        }
    }
}
