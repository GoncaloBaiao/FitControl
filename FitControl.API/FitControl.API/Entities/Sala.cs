namespace FitControl.API.Entities;

public class Sala : BaseEntity
{
    public string Nome { get; set; }
    public ICollection<Aula> Aulas {get; set; } = new List<Aula>();
}