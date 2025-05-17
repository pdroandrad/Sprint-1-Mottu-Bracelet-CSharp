using Microsoft.EntityFrameworkCore;

namespace MottuBracelet.Model
{
    [Owned]
    public class Endereco
    {
        public string Logradouro { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }
}
