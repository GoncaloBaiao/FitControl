using FitControl.API.Entities;

namespace FitControl.API.Models;

public class SocioDto
{
    public int Id { get; set; }
    
    public int TipoPlanoId { get; set; }
    
    public TipoPlano TipoPlano { get; set; }
    
    public string Nome { get; set; }
    
    public DateTime DataNascimento { get; set; }
    
    public int GeneroId { get; set; }
    
    public Genero Genero { get; set; }
   
    public float Altura { get; set; }
    
    public float Peso { get; set; }
    
    public string Email { get; set; }
    
    public string ContactoTelefonico { get; set; }
    
    public DateTime InicioSubscricao { get; set; }
    
    public DateTime FimSubscricao { get; set; }
    
    public bool IsDeleted { get; set; }
}