using System.ComponentModel.DataAnnotations;

namespace MottuBracelet.Model
{
    public class Patio
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required int CapacidadeMaxima { get; set; }
        public required string AdministradorResponsavel { get; set; }
        public required Endereco Endereco { get; set; } = new Endereco();
    }
}
