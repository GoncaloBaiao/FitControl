using FitControl.API.Entities;

namespace FitControl.API.Models;

public class InscricaoDto
{
    public int Id { get; set; }
    
    public int AulaId { get; set; }
    
    public Aula Aula { get; set; }
    
    public int SocioId { get; set; }
    
    public Socio Socio { get; set; }
    
    public DateTime DataInscricao { get; set; }
    
    public DateTime DataCancelamento { get; set; }
    
    public bool IsDeleted { get; set; }
}