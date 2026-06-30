using FitControl.API.Entities;

namespace FitControl.API.Models;

public class AulaDto
{
    public int Id { get; set; }
    
    public int ModalidadeId { get; set; }
    
    public Modalidade Modalidade { get; set; }
    
    public int InstrutorId { get; set; }
    
    public Instrutor Instrutor { get; set; }
    
    public int SalaId { get; set; }
    
    public Sala Sala { get; set; }
    
    public string Nome {get; set;}
    
    public DateTime HoraInicio  {get; set;}
    
    public DateTime HoraFim {get; set;}
    
    public int Capacidade {get; set;}
    
    public bool IsDeleted {get; set;}
}