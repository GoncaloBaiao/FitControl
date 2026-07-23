using System.ComponentModel.DataAnnotations;
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
    
    [StringLength(20, ErrorMessage = "O nome do sócio deve ter no máximo 20 caracteres.")]
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    
    [JsonPropertyName("dataNascimento")]
    public DateTime? DataNascimento { get; set; }
    
    [JsonPropertyName("generoId")]
    public int GeneroId { get; set; }
    
    [JsonPropertyName("genero")]
    public Genero Genero { get; set; }
    
    [Range(1, 3, ErrorMessage = "A altura deve ter valores entre 1m e 3m")]
    [JsonPropertyName("altura")]
    public float Altura { get; set; }
    
    [Range(35, 200, ErrorMessage = "O peso deve ter valores entre 35kg a 200kg")]
    [JsonPropertyName("peso")]
    public float Peso { get; set; }
    
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [RegularExpression(@"^(9\d{8}|2\d{8})$", 
        ErrorMessage = "O contacto telefónico deve ser um número português válido (9 dígitos).")]
    [JsonPropertyName("contactoTelefonico")]
    public string ContactoTelefonico { get; set; }
    
    [JsonPropertyName("inicioSubscricao")]
    public DateTime? InicioSubscricao { get; set; }
    
    [JsonPropertyName("fimSubscricao")]
    public DateTime? FimSubscricao { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}