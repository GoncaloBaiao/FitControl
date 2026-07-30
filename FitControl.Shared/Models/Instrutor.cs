using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Instrutor
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [StringLength(20, ErrorMessage = "O nome do instrutor deve ter no máximo 20 caracteres.")]
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    
    [JsonPropertyName("dataNascimento")]
    public DateTime? DataNascimento { get; set; }
    
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [RegularExpression(@"^(9\d{8}|2\d{8})$", 
        ErrorMessage = "O contacto telefónico deve ser um número português válido (9 dígitos).")]
    [JsonPropertyName("contactoTelefonico")]
    public string ContactoTelefonico { get; set; }
   
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}