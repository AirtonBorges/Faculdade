using System.Text.Json.Serialization;

namespace Trabalho.M3.OrganizadorMidia.Entites;

public class Midia
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Sinopse { get; set; }
    public DateTime DataLancamento { get; set; }
    public string Plataforma { get; set; }
    public string Url { get; set; }
}