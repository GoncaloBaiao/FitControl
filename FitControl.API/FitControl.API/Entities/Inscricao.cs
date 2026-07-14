namespace FitControl.API.Entities;

public class Inscricao : BaseEntity
{
    public int AulaId { get; set; }
    public Aula Aula { get; set; }
    public int SocioId { get; set; }
    public Socio Socio { get; set; }
    public DateTime DataInscricao { get; set; }
    public DateTime DataCancelamento { get; set; }
}