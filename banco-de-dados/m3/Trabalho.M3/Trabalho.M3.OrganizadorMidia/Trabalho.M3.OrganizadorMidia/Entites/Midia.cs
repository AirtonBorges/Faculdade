using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho.M3.OrganizadorMidia.Entites;

[Table("Midia")]
public class Midia
{
    [Key]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Sinopse { get; set; }
    public DateTime DataLancamento { get; set; }
    public string Plataforma { get; set; }
    public string? Url { get; set; }
}