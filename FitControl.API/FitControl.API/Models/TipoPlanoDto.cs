namespace FitControl.API.Models;

public class TipoPlanoDto
{
    public int Id { get; set; }
    
    public string Designacao { get; set; }
    
    public decimal Preco { get; set; }
    
    public bool IsDeleted { get; set; }
}