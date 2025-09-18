using MottuBracelet.Model;

namespace MottuBracelet.DTO
{
    public class DispositivoHateoasDto
    {
        public int Id { get; set; }
        public string StatusDispositivo { get; set; } = string.Empty;
        public int? MotoId { get; set; }
        public int? PatioId { get; set; }
        public List<LinkDto> Links { get; set; } = new();
    }
}
