namespace FitControl.API.Models;

public class InstrutorDto
{
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public DateOnly DataNascimento { get; set; }
    
    public string Email { get; set; }
    
    public string ContactoTelefonico { get; set; }
   
    public bool IsDeleted { get; set; }
}