using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class NivelDificuldade
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [StringLength(15, ErrorMessage = "O nível de dificuldade deve ter no máximo 15 caracteres.")]
    [JsonPropertyName("nivel")]
    public string Nivel { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}