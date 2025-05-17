using System.Text.Json.Serialization;

namespace MottuBracelet.Model
{
    public class HistoricoPatio
    {
        public int Id { get; set; }
        public int? MotoId { get; set; }
        [JsonIgnore]
        public Moto? Moto { get; set; }

        public int? PatioId { get; set; }
        [JsonIgnore]
        public Patio? Patio { get; set; }
        public required DateTime DataEntrada { get; set; }
        public DateTime ?DataSaida { get; set; }
    }
}
