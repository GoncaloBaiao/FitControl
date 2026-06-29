namespace FitControl.API.Entities;

public class TipoPlano : BaseEntity
{
    public string Designacao { get; set; }
    public decimal Preco { get; set; }
    public ICollection<Socio> Socios {get; set; } = new List<Socio>();
}