using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Instrutor
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    
    [JsonPropertyName("dataNascimento")]
    public DateOnly DataNascimento { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("contactoTelefonico")]
    public string ContactoTelefonico { get; set; }
   
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}