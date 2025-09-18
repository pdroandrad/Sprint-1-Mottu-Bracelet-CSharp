using MottuBracelet.Model;

namespace MottuBracelet.DTO
{
    public class MotoHateoasDto
    {
        public int Id { get; set; }
        public string? Imei { get; set; }
        public string? Placa { get; set; }
        public List<LinkDto> Links { get; set; } = new();
    }
}
