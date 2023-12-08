using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho.M3.OrganizadorMidia.Entites;

[Table("Pessoa")]
public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
}