using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Socio
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("tipoPlanoId")]
    public int TipoPlanoId { get; set; }
    
    [JsonPropertyName("tipoPlano")]
    public TipoPlano TipoPlano { get; set; }
    
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    
    [JsonPropertyName("dataNascimento")]
    public DateTime DataNascimento { get; set; }
    
    [JsonPropertyName("genero")]
    public string Genero { get; set; }
    
    [JsonPropertyName("altura")]
    public float Altura { get; set; }
    
    [JsonPropertyName("peso")]
    public float Peso { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("contactoTelefonico")]
    public string ContactoTelefonico { get; set; }
    
    [JsonPropertyName("inicioSubscricao")]
    public DateTime InicioSubscricao { get; set; }
    
    [JsonPropertyName("fimSubscricao")]
    public DateTime FimSubscricao { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}