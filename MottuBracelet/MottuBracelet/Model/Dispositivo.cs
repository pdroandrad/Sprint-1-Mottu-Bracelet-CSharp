using System.Text.Json.Serialization;

namespace MottuBracelet.Model
{
    public class Dispositivo
    {
        public int Id { get; set; }
        public required string StatusDispositivo { get; set; }

        public int? MotoId { get; set; }

        [JsonIgnore]
        public Moto? Moto { get; set; }

        public int? PatioId { get; set; }

        [JsonIgnore]
        public Patio? Patio { get; set; }
    }
}