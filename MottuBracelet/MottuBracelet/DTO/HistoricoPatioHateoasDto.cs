namespace MottuBracelet.DTO
{
    public class HistoricoPatioHateoasDto
    {
        public int Id { get; set; }
        public int? MotoId { get; set; }
        public int? PatioId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public List<LinkDto> Links { get; set; } = new();
    }
}
