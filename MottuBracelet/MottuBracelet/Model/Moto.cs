using MottuBracelet.Model;
using System.Text.Json.Serialization;

public class Moto
{
    public int Id { get; set; }
    public string? Imei { get; set; }
    public string? Placa { get; set; }

    public int? PatioId { get; set; }

    [JsonIgnore]
    public Patio? Patio { get; set; }

    public int? DispositivoId { get; set; }

    [JsonIgnore]
    public Dispositivo? Dispositivo { get; set; }
}