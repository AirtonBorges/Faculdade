namespace Trabalho.M3.OrganizadorMidia.Entites;

public class Agendamento
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdMidia { get; set; }
    public DateTime DataAgendamento { get; set; }
    public string Estado { get; set; }
}