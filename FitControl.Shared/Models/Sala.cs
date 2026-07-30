using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Sala
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [StringLength(20, ErrorMessage = "O nome da sala deve ter no máximo 20 caracteres.")]
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}