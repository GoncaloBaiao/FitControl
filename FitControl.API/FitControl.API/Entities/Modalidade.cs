namespace FitControl.API.Entities;

public class Modalidade : BaseEntity
{
    public int NivelDificuldadeId { get; set; }
    public NivelDificuldade NivelDificuldade { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public ICollection<Aula> Aulas {get; set; } = new List<Aula>();
    public ICollection<InstrutorModalidade> IntrutorModalidades {get; set; } = [];
}