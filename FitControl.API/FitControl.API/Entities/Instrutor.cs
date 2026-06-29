namespace FitControl.API.Entities;

public class Instrutor : BaseEntity
{
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Email { get; set; }
    public string ContactoTelefonico { get; set; }
    public ICollection<Aula> Aulas {get; set; } = new List<Aula>();
    public ICollection<InstrutorModalidade> IntrutorModalidades {get; set; } = [];
}