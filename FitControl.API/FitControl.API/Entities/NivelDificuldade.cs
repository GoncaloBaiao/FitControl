namespace FitControl.API.Entities;

public class NivelDificuldade : BaseEntity
{
    public string Nivel { get; set; }
    public ICollection<Modalidade> Modalidades {get; set; } = new List<Modalidade>();
}