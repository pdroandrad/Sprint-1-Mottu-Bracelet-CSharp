using MottuBracelet.Model;

namespace MottuBracelet.DTO
{
    public class PatioHateoasDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CapacidadeMaxima { get; set; }
        public string AdministradorResponsavel { get; set; } = string.Empty;
        public Endereco Endereco { get; set; } = new Endereco();
        public List<LinkDto> Links { get; set; } = new();
    }
}
