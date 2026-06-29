namespace FitControl.API.Entities;

public class Socio : BaseEntity
{
    public int TipoPlanoId { get; set; }
    public TipoPlano TipoPlano { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Email { get; set; }
    public string ContactoTelefonico { get; set; }
    public DateOnly InicioSubscricao { get; set; }
    public DateOnly FimSubscricao { get; set; }
    public ICollection<Inscricao> Inscricaos {get; set; } = [];
}